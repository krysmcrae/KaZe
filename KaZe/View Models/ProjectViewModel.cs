using KaZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaZe.View_Models
{
    public class ProjectViewModel
    {
        public Project project { get; set; }
        public ApplicationUser projectManager { get; set; }
        public ICollection<ApplicationUser> personnelDev { get; set; }

        public ProjectViewModel()
        {
            personnelDev = new HashSet<ApplicationUser>();

        }

    }
}