using KaZe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KaZe.Helpers
{
    public class TicketsHelper:IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper rolesHelper = new RolesHelper();
        public bool IsUserAssignedToTicket(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            var flag = ticket.AssignedToUserId == userId;
            return (flag);
        }
        public bool IsUserOwnerOfTicket(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            var flag = ticket.SubmittedByUserId == userId;
            return (flag);
        }

        public void AddOwnerOfTicket(string userId, int ticketId)
        {
            if (!IsUserOwnerOfTicket(userId, ticketId))
            {
                Ticket ticket = db.Tickets.Find(ticketId);
                var newUser = db.Users.Find(userId);
                ticket.SubmittedByUser = newUser;
                ticket.SubmittedByUserId = userId;
                db.SaveChanges();
            }
        }
        public void AssignDevToTicket(string userId, int ticketId)
        {
            if (!IsUserAssignedToTicket(userId, ticketId))
            {
                Ticket ticket = db.Tickets.Find(ticketId);
                var newUser = db.Users.Find(userId);
                ticket.AssignedToUser = newUser;
                ticket.AssignedToUserId = userId;
                db.SaveChanges();
            }
        }
        public void UnassignDevToTicket(string userId, int ticketId)
        {
            if (IsUserAssignedToTicket(userId, ticketId))
            {
                Ticket ticket = db.Tickets.Find(ticketId);
                var newUser = db.Users.Find(userId);
                ticket.AssignedToUser = newUser;
                ticket.AssignedToUserId = userId;
                db.Entry(ticket).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        }
        public ApplicationUser AssignedDev(int ticketId)
        {
            return db.Tickets.Find(ticketId).AssignedToUser;
        }
        public ICollection<ApplicationUser> DevsNotAssigned(int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            var devs = rolesHelper.UsersInRole("Developer");
            var unassignedDevs = devs.Where(d => d.Id != ticket.AssignedToUserId).ToList();
            return unassignedDevs;
        }
        public ICollection<Ticket> submittedByTickets(string userId)
        {
            var tickets = db.Tickets.Where(t => t.SubmittedByUserId == userId).ToList();
            return (tickets);
        }

        public ICollection<Ticket> assignedToTickets(string userId)
        {
            var tickets = db.Tickets.Where(t => t.AssignedToUserId == userId).ToList();
            return (tickets);
        }
        public ICollection<Ticket> assignedTickets()
        {
            var tickets = db.Tickets.Where(t => t.AssignedToUserId != null).ToList();
            return (tickets);
        }

        public ICollection<Ticket> unassignedTickets()
        {
            var tickets = db.Tickets.Where(t => t.AssignedToUserId == null).ToList();
            return (tickets);
        }

        public Ticket TicketProperties(Ticket ticket)
        {
            ticket.Project = db.Projects.Find(ticket.ProjectId);
            ticket.TicketType = db.TicketTypes.Find(ticket.TicketTypeId);
            ticket.TicketPriority = db.TicketPriorities.Find(ticket.TicketPriorityId);
            ticket.TicketStatus = db.TicketStatus.Find(ticket.TicketStatusId);
            ticket.SubmittedByUser = db.Users.Find(ticket.SubmittedByUserId);
            ticket.AssignedToUser = db.Users.Find(ticket.AssignedToUserId);
            Dispose();
            return ticket;
        }

        public void Dispose()
        {
            db.Dispose();
        }
        
    }



}
