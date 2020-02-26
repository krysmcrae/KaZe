using KaZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaZe.Helpers
{
    public class HistoriesHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void ChangeDetector(Ticket oldTicket, Ticket newTicket, string userId, Ticket dbTicket)
        {
            var user = db.Users.Find(userId);
            if (oldTicket.Title != newTicket.Title)
            {
                TicketHistory titleRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Title",
                    OldValue = oldTicket.Title,
                    NewValue = newTicket.Title,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(titleRecord);
            }
            if (oldTicket.Description != newTicket.Description)
            {
                TicketHistory descriptionRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Description",
                    OldValue = oldTicket.Description,
                    NewValue = newTicket.Description,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(descriptionRecord);

            }
            if (oldTicket.Project != newTicket.Project)
            {
                TicketHistory projectRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Project",
                    OldValue = oldTicket.Project.Name,
                    NewValue = newTicket.Project.Name,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(projectRecord);

            }
            if (oldTicket.TicketType != newTicket.TicketType)
            {
                TicketHistory ticketTypeRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Ticket Type",
                    OldValue = oldTicket.TicketType.Name,
                    NewValue = newTicket.TicketType.Name,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(ticketTypeRecord);
            }
            if (oldTicket.TicketPriority != newTicket.TicketPriority)
            {
                TicketHistory ticketPriorityRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Ticket Priority",
                    OldValue = oldTicket.TicketPriority.Name,
                    NewValue = newTicket.TicketPriority.Name,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(ticketPriorityRecord);
            }
            if (oldTicket.TicketStatus != newTicket.TicketStatus)
            {
                TicketHistory ticketStatusRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Ticket Status",
                    OldValue = oldTicket.TicketStatus.Name,
                    NewValue = newTicket.TicketStatus.Name,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(ticketStatusRecord);
            }
            if (oldTicket.AssignedToUser != newTicket.AssignedToUser)
            {
                TicketHistory devRecord = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = "Developer",
                    OldValue = oldTicket.AssignedToUser.DisplayName,
                    NewValue = newTicket.AssignedToUser.DisplayName,
                    Changed = DateTime.Now,
                    UserId = user.Id
                };
                dbTicket.TicketHistories.Add(devRecord);
            }
        }
    }
}