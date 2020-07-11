using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Models.ViewModels.Management.BranchNetwork
{
    public class Create
    {
        [Required]
        public string Name { get; set; }
        public string ReturnUrl { get; set; }
    }
}
