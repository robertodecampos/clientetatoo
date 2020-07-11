using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.Utils;

namespace TatooReport.Policies.Requirements
{
    public class HasAccessInBranchNetworkRequirement : IAuthorizationRequirement { }
}
