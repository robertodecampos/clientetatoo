using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TatooReport.Models;

namespace TatooReport.Utils
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly Connection connection;

        public RoleStore(Connection connection)
        {
            this.connection = connection;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await new Task<IdentityResult>(
                () =>
                {
                    role.Save(connection, null);

                    return IdentityResult.Success;
                }
            );
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await new Task<IdentityResult>(
                () =>
                {
                    role.Delete(connection, null);
                    return IdentityResult.Success;
                }
            );
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await new Task<Role>(
                () =>
                {
                    return Role.FindById(int.Parse(roleId), connection, null);
                }
            );
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await new Task<Role>(
                () =>
                {
                    return Role.FindByNormalizedName(normalizedRoleName, connection, null);
                }
            );
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(role.NormalizedName);
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(role.Id.ToString());
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await new Task(
                () =>
                {
                    role.NormalizedName = normalizedName;
                }
            );
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await new Task(
                () =>
                {
                    role.Name = roleName;
                }
            );
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await new Task<IdentityResult>(
                () =>
                {
                    role.Save(connection, null);
                    return IdentityResult.Success;
                }
            );
        }

        public void Dispose() { }
    }
}
