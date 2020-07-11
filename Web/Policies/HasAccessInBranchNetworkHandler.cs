using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.Models;
using TatooReport.Policies.Requirements;
using TatooReport.Utils;

namespace TatooReport.Policies
{
    public class HasAccessInBranchNetworkHandler : AuthorizationHandler<HasAccessInBranchNetworkRequirement>
    {
        private Connection Connection { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }
        private UserManager<User> UserManager { get; set; }

        public HasAccessInBranchNetworkHandler(Connection connection, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            Connection = connection;
            HttpContextAccessor = httpContextAccessor;
            UserManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessInBranchNetworkRequirement requirement)
        {
            if (!HttpContextAccessor.HttpContext.Request.Query.ContainsKey("branchNetworkId"))
            {
                return Task.CompletedTask;
            }

            int branchNetworkId = int.Parse(HttpContextAccessor.HttpContext.Request.Query["branchNetworkId"]);

            int userId = int.Parse(UserManager.GetUserId(context.User));

            if (BranchNetworkUser.HasAccessInBranchNetwork(branchNetworkId, userId, Connection, null))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
