﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KaZe.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarPath { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public ApplicationUser()
        {
            Projects = new HashSet<Project>();

            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<KaZe.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.Ticket> Tickets { get; set; }


        public System.Data.Entity.DbSet<KaZe.Models.TicketPriority> TicketPriorities { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.TicketStatus> TicketStatus { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.TicketHistory> TicketHistories { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.TicketComment> TicketComments { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.TicketAttachment> TicketAttachments { get; set; }

        public System.Data.Entity.DbSet<KaZe.Models.TicketNotification> TicketNotifications { get; set; }
    }
}