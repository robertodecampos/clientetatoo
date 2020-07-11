using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TatooReport.Exceptions;
using TatooReport.Models;

namespace TatooReport.Utils
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
    {

        private readonly Connection connection;

        public UserStore(Connection connection)
        {
            this.connection = connection;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var task = new Task<IdentityResult>(
                () =>
                {
                    user.Save(connection, null);
                    return IdentityResult.Success;
                }
            );

            task.Start();

            await task;

            return task.Result;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.Delete(connection, null);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(User.FindById(int.Parse(userId), connection, null));
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(User.FindByNormalizedName(normalizedUserName, connection, null));
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.Save(connection, null);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Factory.StartNew(
                () =>
                {
                    using (Role role = Role.FindByNormalizedName(roleName, connection, null))
                    using (var roleUser = new UserRole())
                    {
                        if (role == null)
                            throw new UserException($"Não foi encontrado nenhum nível de acesso com o nome '{roleName}'!");

                        roleUser.RoleId = role.Id;
                        roleUser.UserId = user.Id;

                        roleUser.Save(connection, null);
                    }
                }
            );
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await new Task(
                () =>
                {
                    using (Role role = Role.FindByNormalizedName(roleName, connection, null))
                    using (UserRole userRole = UserRole.FindByUserIdAndRoleId(user.Id, role.Id, connection, null))
                    {
                        if (role == null)
                            throw new UserException($"Não foi encontrado nenhum nível de acesso com o nome '{roleName}'!");

                        if (userRole == null)
                            throw new UserException($"Não foi encontrado nenhum nível de acesso com o nome '{roleName}' para o usuário '{user.Nome}'!");

                        userRole.Delete(connection, null);
                    }
                }
            );
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(UserRole.FindRoleNameByUserId(user.Id, connection, null));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new Task<bool>(
                () =>
                {
                    using (Role role = Role.FindByNormalizedName(roleName, connection, null))
                    {
                        if (role == null)
                            throw new UserException($"Não foi encontrado nenhum nível de acesso com o nome '{roleName}'!");

                        return UserRole.ExistsUserInRole(user.Id, role.Id, connection, null);
                    }
                }
            );
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Role role = Role.FindByNormalizedName(roleName.ToUpper(), connection, null);

            if (role == null)
                throw new UserException($"Não foi encontrado nenhum nível de acesso com o nome '{roleName}'!");

            return Task.FromResult(User.FindInRole(role.Id, connection, null));
        }

        public void Dispose() { }
    }
}
