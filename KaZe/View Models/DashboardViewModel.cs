using KaZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity;
using System.Security.Principal;


namespace KaZe.View_Models
{
    public class DashboardViewModel
    {

        public ApplicationUser user { get; set; }
        public string role { get; set; }
        public ICollection<Project> assignedProjects { get; set; }

        public ICollection<Project> projects { get; set; }
        public ICollection<Ticket> assignedTickets { get; set; }
        public ICollection<Ticket> unassignedTickets { get; set; }
        public ICollection<Ticket> tickets { get; set; }
        public ICollection<ApplicationUser> personnelPM { get; set; }
        public ICollection<ApplicationUser> personnelDev { get; set; }
        public ICollection<ApplicationUser> personnelSub { get; set; }
        public ICollection<ApplicationUser> personnelUnassigned { get; set; }


        public DashboardViewModel()
        {
            assignedProjects = new HashSet<Project>();
            projects = new HashSet<Project>();
            tickets = new HashSet<Ticket>();
            assignedTickets = new HashSet<Ticket>();
            unassignedTickets = new HashSet<Ticket>();
            personnelPM = new HashSet<ApplicationUser>();
            personnelDev = new HashSet<ApplicationUser>();
            personnelSub = new HashSet<ApplicationUser>();
            personnelUnassigned = new HashSet<ApplicationUser>();
        }


    }
}