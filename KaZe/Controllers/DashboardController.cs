using KaZe.Helpers;
using KaZe.Models;
using KaZe.View_Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KaZe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper rolesHelper = new RolesHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();
        private TicketsHelper ticketsHelper = new TicketsHelper();

        // GET: Dashboard
        public ActionResult Index()
        {


            var model = new DashboardViewModel();
            var userId = User.Identity.GetUserId();
            model.user = db.Users.Find(userId);
            var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
            model.role = myRole;
            switch (myRole)
            {
                case "Global Admin":
                case "Demo Admin":
                case "Admin":
                    model.projects = db.Projects.ToList();
                    model.tickets = db.Tickets.ToList();
                    model.personnelPM = rolesHelper.UsersInRole("Project Manager");
                    model.personnelDev = rolesHelper.UsersInRole("Developer");
                    model.personnelSub = rolesHelper.UsersInRole("Submitter");
                    break;
                case "Project Manager":
                    model.projects = db.Projects.ToList();
                    model.assignedProjects = model.user.Projects;
                    model.assignedTickets = ticketsHelper.assignedTickets();
                    model.unassignedTickets = ticketsHelper.unassignedTickets();
                    model.personnelDev = rolesHelper.UsersInRole("Developer");
                    model.personnelSub = rolesHelper.UsersInRole("Submitter");
                    break;
                case "Developer":

                    model.assignedProjects = model.user.Projects;
                    model.assignedTickets = ticketsHelper.assignedToTickets(userId);
                    break;
                case "Submitter":
                    model.projects = model.user.Projects;
                    model.tickets = ticketsHelper.submittedByTickets(userId);
                    break;
            }




            return View(model);
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(string id)
        {
            var model = new DashboardViewModel();
            model.user = db.Users.Find(id);
            var myRole = rolesHelper.ListUserRoles(id).FirstOrDefault();
            model.role = myRole;
            switch (myRole)
            {
                case "Global Admin":
                case "Demo Admin":
                case "Admin":
                    model.assignedProjects = db.Projects.ToList();
                    model.assignedTickets = db.Tickets.ToList();

                    break;
                case "Project Manager":
                    model.assignedProjects = model.user.Projects;

                    break;
                case "Developer":
                    model.assignedProjects = model.user.Projects;
                    model.assignedTickets = ticketsHelper.assignedToTickets(id);
                    break;
                case "Submitter":
                    model.assignedProjects = model.user.Projects;
                    model.tickets = ticketsHelper.submittedByTickets(id);
                    break;
            }
            return View(model);
        }






    }
}
