using KaZe.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaZe.Helpers
{
    public static class NotificationManager
    {
         
        public static void ManageTicketNotifications(Ticket oldTicket, Ticket newTicket)
        {
            ManageGeneralAssignmentNotification(oldTicket, newTicket);
            ManagePropertyChangeNotification(oldTicket, newTicket);
            if (oldTicket.AssignedToUserId != newTicket.AssignedToUserId)
            {

            }
        }

        private static void ManageGeneralAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var assigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUserId != null;
            var unassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            var reassigned = newTicket.AssignedToUserId != null && newTicket.AssignedToUserId != oldTicket.AssignedToUserId;


            if (assigned) 
            {
                GenerateNotification(new TicketNotification
                {
                    RecipientId = newTicket.AssignedToUserId,
                    NotificationBody = $"You have been assigned to Ticket Id {newTicket.Id}",
                    TicketId = newTicket.Id
                });
            }else if(unassigned)
            {
                GenerateNotification(new TicketNotification
                {
                    RecipientId = oldTicket.AssignedToUserId,
                    NotificationBody = $"You have been unassigned from Ticket Id {newTicket.Id}",
                    TicketId = newTicket.Id
                });
            }
            else if (reassigned)
            {
                GenerateNotification(new TicketNotification
                {
                    RecipientId = newTicket.AssignedToUserId,
                    NotificationBody = $"You have been assigned to Ticket Id {newTicket.Id}",
                    TicketId = newTicket.Id
                }); 
                GenerateNotification(new TicketNotification
                {
                    RecipientId = oldTicket.AssignedToUserId,
                    NotificationBody = $"You have been unassigned from Ticket Id {newTicket.Id}",
                    TicketId = newTicket.Id
                });
            }
        }
        private static void ManagePropertyChangeNotification(Ticket oldTicket, Ticket newTicket)
        {
            if (newTicket.AssignedToUserId == null)
                return;
            if (oldTicket.Title != newTicket.Title)
            {
                GenerateNotification(new TicketNotification
                {
                    
                    RecipientId = newTicket.AssignedToUserId,
                    NotificationBody = $"The title has changed for Ticket Id {newTicket.Id} from {oldTicket.Title} to {newTicket.Title}",
                    TicketId = newTicket.Id
                });
            }

            ////Copy above code for each property of Ticket
           
        }
        public static void ManageAttachmentNotifications(TicketAttachment ticketAttachment)
        {

        }

        private static void GenerateNotification(TicketNotification notification)
        {
            var db = new ApplicationDbContext();
            var newNotification = new TicketNotification
            {
                Created = DateTime.Now,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                RecipientId = notification.RecipientId,
                NotificationBody = notification.NotificationBody,
                IsRead = false,
                TicketId = notification.TicketId
            };

            db.TicketNotifications.Add(newNotification);
            db.SaveChanges();
    }

    }

}