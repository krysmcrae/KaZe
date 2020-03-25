using KaZe.Helpers;
using KaZe.View_Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KaZe.Models
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper rolesHelper = new RolesHelper();
        private HistoriesHelper historyHelp = new HistoriesHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.Project).Include(t => t.SubmittedByUser).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        public ActionResult Histories(int? id)
        {
            var ticket = db.Tickets.Where(t=>t.Id==id).FirstOrDefault();
            return View(ticket);
        }

        public ActionResult MyIndex()
        {

            var myUserId = User.Identity.GetUserId();
            var myRole = rolesHelper.ListUserRoles(myUserId).FirstOrDefault();
            var myTickets = new List<Ticket>();
            //modify if statement to case switch switch(myRole){case "Admin":...code...break; case "Developer":...code... break; default:...code...break;
            switch (myRole)
            {
                case "Admin":
                case "Global Admin":
                case "Demo Admin":
                    myTickets = db.Tickets.ToList();
                    break;

                case "Project Manager":
                    var me = db.Users.Find(myUserId);
                    var myProjects = me.Projects;
                    myTickets = myProjects.SelectMany(p => p.Tickets).ToList();
                    break;

                case "Developer":
                    myTickets = db.Tickets.Where(t => t.AssignedToUserId == myUserId).ToList();
                    break;
                case "Submitter":
                    myTickets = db.Tickets.Where(t => t.SubmittedByUserId == myUserId).ToList();

                    break;
            }
            return View(myTickets);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            var  model = new TicketViewModel();
            if (id == null)
            {
                return  new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.ticket = db.Tickets.Find(id);
            if (model.ticket == null)
            {
                return HttpNotFound();
            }
            model.projMan = projectsHelper.WhoIsProjMan(model.ticket.ProjectId);
            return View(model);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Submitter, Global Admin, Demo Admin")]
        public ActionResult Create()
        {
            
            ViewBag.AssignedToUserId = new SelectList(rolesHelper.UsersInRole("Developer"), "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.SubmittedByUserId = new SelectList(rolesHelper.UsersInRole("Submitter"), "Id", "FirstName");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Submitter, Global Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,SubmittedByUserId,AssignedToUserId")] Ticket ticket)
        {
            var model = new TicketViewModel();
            ticket.Created = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Details","Projects", new { id = ticket.ProjectId});
            }

            ViewBag.AssignedToUserId = new SelectList(rolesHelper.UsersInRole("Developer"), "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.SubmittedByUserId = new SelectList(rolesHelper.UsersInRole("Submitter"), "Id", "FirstName");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Submitter, Global Admin, Demo Admin, Project Manager, Admin, Developer")]
        public ActionResult Edit(int? id)
        {
            var myUserId = User.Identity.GetUserId();
            var myRole = rolesHelper.ListUserRoles(myUserId).FirstOrDefault();
            var ticket = db.Tickets.Find(id);
            var myTicket = db.Tickets.Where(t => t.Id == id).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ticket == null)
            {
                return HttpNotFound();
            }
           

                switch(myRole){
                case "Project Manager":
                    var me = db.Users.Find(myUserId);
                    var myProjects = me.Projects;
                    //if
                    //myTicket
                    break;
                //case "Developer": 
                //     return RedirectToAction(NotAllowed...needs to be created...); 
                //    break;
            }
            //switch case on my role. case Developrt:if(ticket.AssignedToUserId!=myUserId) return RedirectToAction(NotAllowed...needs to be created...);break;
            //case manager is special: if(!db.Users.Find(myUserId).projects.selectmany(p=>p.tickets).select(t=>t.id).contains(ticket.Id)

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.SubmittedByUserId = new SelectList(db.Users, "Id", "FirstName", ticket.SubmittedByUserId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,AssignedToUserId,SubmittedByUserId")] Ticket ticket)
        {   ticket.Updated = DateTime.Now;
            var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
         
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                var newTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                var userId = User.Identity.GetUserId();
                historyHelp.ChangeDetector(oldTicket, newTicket, userId,ticket);
                NotificationManager.ManageTicketNotifications(oldTicket,newTicket);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.SubmittedByUserId = new SelectList(db.Users, "Id", "FirstName", ticket.SubmittedByUserId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AssignTicket(int? id)
        {
            
            var ticket = db.Tickets.Find(id);
            var users = rolesHelper.UsersInRole("Developer").ToList();
            ViewBag.AssignedToUserId = new SelectList(users, "Id", "FullName",
            ticket.AssignedToUserId);
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTicket(Ticket model)
        {
            var ticket = db.Tickets.Find(model.Id);
            ticket.AssignedToUserId = model.AssignedToUserId;
            db.SaveChanges();
            var callbackUrl = Url.Action("Details", "Tickets", new { id = ticket.Id },
            protocol: Request.Url.Scheme);
            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                ApplicationUser user = db.Users.Find(model.AssignedToUserId);
                msg.Body = "You have been assigned a new Ticket." + Environment.NewLine +
                "Please click the following link to view the details " +
                "<a href=\"" + callbackUrl + "\">NEW TICKET</a>";
                msg.Destination = user.Email;
                msg.Subject = "Invite to Household";
                await ems.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
