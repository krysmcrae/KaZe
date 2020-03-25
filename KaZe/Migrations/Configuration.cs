namespace KaZe.Migrations
{
    using KaZe.Helpers;
    using KaZe.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<KaZe.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "KaZe.Models.ApplicationDbContext";
        }

        protected override void Seed(KaZe.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];
            ProjectsHelper projectsHelper = new ProjectsHelper();
            TicketsHelper ticketsHelper = new TicketsHelper();

            #region Seeding Users with roles

            #region Add Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Global Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Global Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            if (!context.Roles.Any(r => r.Name == "Demo Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo Admin" });
            }
            #endregion

            #region Add Users to fill the roles
            if (!context.Users.Any(u => u.Email == "krysmcrae@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "krysmcrae@Mailinator.com",
                    UserName = "krysmcrae@Mailinator.com",
                    FirstName = "Krys",
                    LastName = "McRae",
                    DisplayName = "Global Admin"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "jtwich@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "jTwitch@Mailinator.com",
                    UserName = "jTwitch@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twitchell",
                    DisplayName = "Developer"
                }, "pretzel3!");
            }
            if (!context.Users.Any(u => u.Email == "DemoAdmin88@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "DemoAdmin88@Mailinator.com",
                    UserName = "DemoAdmin88@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "Demo Admin"
                }, demoPassword);
            }
            if (!context.Users.Any(u => u.Email == "KazeKage@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "KazeKage@Mailinator.com",
                    UserName = "KazeKage@Mailinator.com",
                    FirstName = "Gaara",
                    LastName = "Admin",
                    DisplayName = "KazeKage"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "LadyChiyo@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "LadyChiyo@Mailinator.com",
                    UserName = "LadyChiyo@Mailinator.com",
                    FirstName = "Lady C.",
                    LastName = "Project Manager",
                    DisplayName = "Chiyo"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Kankuro@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Kankuro@Mailinator.com",
                    UserName = "Kankuro@Mailinator.com",
                    FirstName = "Kankuro",
                    LastName = "Developer",
                    DisplayName = "Kankuro"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "Temari@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Temari@Mailinator.com",
                    UserName = "Temari@Mailinator.com",
                    FirstName = "Temari",
                    LastName = "Developer",
                    DisplayName = "Temari"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "Shinki@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Shinki@Mailinator.com",
                    UserName = "Shinki@Mailinator.com",
                    FirstName = "Shinki",
                    LastName = "Submitter",
                    DisplayName = "Shinki"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Kakashi@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Kakashi@Mailinator.com",
                    UserName = "Kakashi@Mailinator.com",
                    FirstName = "Kakashi",
                    LastName = "Project Manager",
                    DisplayName = "Kakashi"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "Sakura@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Sakura@Mailinator.com",
                    UserName = "Sakura@Mailinator.com",
                    FirstName = "Sakura",
                    LastName = "Developer",
                    DisplayName = "Sakura"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "Naruto@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Naruto@Mailinator.com",
                    UserName = "Naruto@Mailinator.com",
                    FirstName = "Naruto",
                    LastName = "Developer",
                    DisplayName = "Naruto"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "Sasuke@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Sasuke@Mailinator.com",
                    UserName = "Sasuke@Mailinator.com",
                    FirstName = "Sasuke",
                    LastName = "Developer",
                    DisplayName = "Sasuke"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "Konohamaru@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Konohamaru@Mailinator.com",
                    UserName = "Konohamaru@Mailinator.com",
                    FirstName = "Konohamaru",
                    LastName = "Submitter",
                    DisplayName = "Konohamaru"
                }, "Abc&123!");
            }
            #endregion

            #region Assign roles to Users
            var globalAdminId = userManager.FindByEmail("krysmcrae@Mailinator.com").Id;
            var adminId = userManager.FindByEmail("KazeKage@Mailinator.com").Id;
            var projManId = userManager.FindByEmail("LadyChiyo@Mailinator.com").Id;
            var dev1Id = userManager.FindByEmail("Kankuro@Mailinator.com").Id;
            var dev2Id = userManager.FindByEmail("Temari@Mailinator.com").Id;
            var dev3Id = userManager.FindByEmail("Sakura@Mailinator.com").Id;
            var submitId = userManager.FindByEmail("Shinki@Mailinator.com").Id;
            var projMan2Id = userManager.FindByEmail("Kakashi@Mailinator.com").Id;
            var dev4Id = userManager.FindByEmail("Naruto@Mailinator.com").Id;
            var dev5Id = userManager.FindByEmail("Sasuke@Mailinator.com").Id;
            var submit2Id = userManager.FindByEmail("Konohamaru@Mailinator.com").Id;
            var demoAdminId = userManager.FindByEmail("DemoAdmin88@Mailinator.com").Id;


            userManager.AddToRole(globalAdminId, "Global Admin");
            userManager.AddToRole(adminId, "Admin");
            userManager.AddToRole(projManId, "Project Manager");
            userManager.AddToRole(projMan2Id, "Project Manager");
            userManager.AddToRole(dev1Id, "Developer");
            userManager.AddToRole(dev2Id, "Developer");
            userManager.AddToRole(dev3Id, "Developer");
            userManager.AddToRole(dev4Id, "Developer");
            userManager.AddToRole(dev5Id, "Developer");
            userManager.AddToRole(submitId, "Submitter");
            userManager.AddToRole(submit2Id, "Submitter");
            userManager.AddToRole(demoAdminId, "Demo Admin");




            #endregion

            #endregion

            #region Seeding Projects
            #region Add Projects
            if (!context.Projects.Any(p => p.Name == "Operation: Sand Retrieval"))
            { context.Projects.Add(new Project { Name = "Operation: Sand Retrieval", Description = "Secure Gaara and Shukaku from the Akatsuki" }); }

            if (!context.Projects.Any(p => p.Name == "Operation: Escort the Bridge Architect"))
            { context.Projects.Add(new Project { Name = "Operation: Escort the Bridge Architect", Description = "Escort Tazuna from the Leaf Village to the Land of Waves" }); }
            context.SaveChanges();
            #endregion

            #region Assign Users to Projects
            var Project1 = context.Projects.Where(p => p.Name == "Operation: Sand Retrieval").First();
            var Project1Id = Project1.Id;

            projectsHelper.SeedUsersToProject(Project1.Id, projManId);
            projectsHelper.SeedUsersToProject(Project1.Id, dev1Id);
            projectsHelper.SeedUsersToProject(Project1.Id, dev2Id);
            projectsHelper.SeedUsersToProject(Project1.Id, dev3Id);
            projectsHelper.SeedUsersToProject(Project1.Id, dev4Id);
            projectsHelper.SeedUsersToProject(Project1.Id, submitId);

            var Project2 = context.Projects.Where(p => p.Name == "Operation: Escort the Bridge Architect").First();
            projectsHelper.SeedUsersToProject(Project2.Id, projMan2Id);
            projectsHelper.SeedUsersToProject(Project2.Id, dev3Id);
            projectsHelper.SeedUsersToProject(Project2.Id, dev4Id);
            projectsHelper.SeedUsersToProject(Project2.Id, dev5Id);
            projectsHelper.SeedUsersToProject(Project2.Id, submit2Id);
            context.SaveChanges();
            #endregion

            #endregion

            #region Seeding Tickets


            #region Seed TicketTypes Table
            //context.TicketTypes.AddOrUpdate(
            //    t => t.Name,
            //    new TicketType { Name = "Defect", Description = "Functionality issue" },
            //    new TicketType { Name = "Enhancement", Description = "Functionality issue" });
            if (!context.TicketTypes.Any(u => u.Name == "Production Fix"))
            { context.TicketTypes.Add(new TicketType { Name = "Production Fix" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Project Task"))
            { context.TicketTypes.Add(new TicketType { Name = "Project Task" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Software Update"))
            { context.TicketTypes.Add(new TicketType { Name = "Software Update" }); }
            context.SaveChanges();

            var ticketType1 = context.TicketTypes.Where(tt => tt.Name == "Production Fix").First();
            var ticketType1Id = ticketType1.Id;
            var ticketType2 = context.TicketTypes.Where(tt => tt.Name == "Project Task").First();
            var ticketType2Id = ticketType2.Id;
            var ticketType3 = context.TicketTypes.Where(tt => tt.Name == "Software Update").First();
            var ticketType3Id = ticketType2.Id;

            #endregion

            #region Seed TicketStatus Table
            if (!context.TicketStatus.Any(u => u.Name == "New"))
            { context.TicketStatus.Add(new TicketStatus { Name = "New" }); }

            if (!context.TicketStatus.Any(u => u.Name == "In Development"))
            { context.TicketStatus.Add(new TicketStatus { Name = "In Development" }); }

            if (!context.TicketStatus.Any(u => u.Name == "Completed"))
            { context.TicketStatus.Add(new TicketStatus { Name = "Completed" }); }
            if (!context.TicketStatus.Any(u => u.Name == "Archived"))
            { context.TicketStatus.Add(new TicketStatus { Name = "Archived" }); }
            context.SaveChanges();

            var ticketStatus1 = context.TicketStatus.Where(ts => ts.Name == "New").First();
            var ticketStatus1Id = ticketStatus1.Id;
            var ticketStatus2 = context.TicketStatus.Where(ts => ts.Name == "In Development").First();
            var ticketStatus2Id = ticketStatus2.Id;
            var ticketStatus3 = context.TicketStatus.Where(ts => ts.Name == "Completed").First();
            var ticketStatus3Id = ticketStatus3.Id;
            #endregion

            #region Seed TicketPriorities Table
            if (!context.TicketPriorities.Any(u => u.Name == "Urgent"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Urgent" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "High"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "High" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Medium"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Medium" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Low"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Low" }); }
            context.SaveChanges();

            var ticketPriority1 = context.TicketPriorities.Where(tp => tp.Name == "Urgent").First();
            var ticketPriority1Id = ticketPriority1.Id;
            var ticketPriority2 = context.TicketPriorities.Where(tp => tp.Name == "High").First();
            var ticketPriority2Id = ticketPriority2.Id;
            var ticketPriority3 = context.TicketPriorities.Where(tp => tp.Name == "Medium").First();
            var ticketPriority3Id = ticketPriority3.Id;
            var ticketPriority4 = context.TicketPriorities.Where(tp => tp.Name == "Low").First();
            var ticketPriority4Id = ticketPriority4.Id;

            #endregion

            #region Add Tickets

            #region Sand Retrieval Tickets

            if (!context.Tickets.Any(t => t.Title == "Fight Sasori"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Fight Sasori",
                    Created = DateTime.Parse("5/1/2019 8:30:11 AM"),
                    ProjectId = Project1Id,
                    TicketTypeId = ticketType1Id,
                    TicketPriorityId = ticketPriority3Id,
                    TicketStatusId = ticketStatus3Id,
                    SubmittedByUserId = submitId,
                    AssignedToUserId = dev1Id

                });


            }
            if (!context.Tickets.Any(t => t.Title == "Defend The Sand Village"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Defend The Sand Village",
                    Created = DateTime.Parse("5/3/2019 4:22:08 PM"),
                    ProjectId = Project1Id,
                    TicketTypeId = ticketType2Id,
                    TicketPriorityId = ticketPriority1Id,
                    TicketStatusId = ticketStatus2Id,
                    SubmittedByUserId = submitId,
                    AssignedToUserId = dev2Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Defeat Sasori"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Defeat Sasori",
                    Created = DateTime.Parse("5/3/2019 5:30:00 PM"),
                    ProjectId = Project1Id,
                    TicketTypeId = ticketType3Id,
                    TicketPriorityId = ticketPriority1Id,
                    TicketStatusId = ticketStatus1Id,
                    SubmittedByUserId = submitId,
                    AssignedToUserId = dev3Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Heal Kankuro"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Heal Kankuro",
                    Created = DateTime.Parse("5/1/2019 3:31:57 PM"),
                    ProjectId = Project1Id,
                    TicketTypeId = ticketType1Id,
                    TicketPriorityId = ticketPriority2Id,
                    TicketStatusId = ticketStatus3Id,
                    SubmittedByUserId = submitId,
                    AssignedToUserId = dev3Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Fight Deidara"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Fight Deidara",
                    Created = DateTime.Parse("5/4/2019 1:51:44 AM"),
                    ProjectId = Project1Id,
                    TicketTypeId = ticketType2Id,
                    TicketPriorityId = ticketPriority1Id,
                    TicketStatusId = ticketStatus2Id,
                    SubmittedByUserId = submitId,
                    AssignedToUserId = dev4Id

                });
            }

            #endregion

            #region Bridge Architect Tickets

            if (!context.Tickets.Any(t => t.Title == "Practice Chakra Control"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Practice Chakra Control",
                    Created = DateTime.Parse("8/6/2018 2:02:09 AM"),
                    ProjectId = Project2.Id,
                    TicketTypeId = ticketType1Id,
                    TicketPriorityId = ticketPriority3Id,
                    TicketStatusId = ticketStatus3Id,
                    SubmittedByUserId = submit2Id,
                    AssignedToUserId = dev4Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Fight Haku "))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Fight Haku ",
                    Created = DateTime.Parse("8/8/2018 7:43:19 AM"),
                    ProjectId = Project2.Id,
                    TicketTypeId = ticketType2Id,
                    TicketPriorityId = ticketPriority1Id,
                    TicketStatusId = ticketStatus2Id,
                    SubmittedByUserId = submit2Id,
                    AssignedToUserId = dev4Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Defend Architect"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Defend Architect",
                    Created = DateTime.Parse("8/8/2018 9:13:44 AM"),
                    ProjectId = Project2.Id,
                    TicketTypeId = ticketType3Id,
                    TicketPriorityId = ticketPriority2Id,
                    TicketStatusId = ticketStatus1Id,
                    SubmittedByUserId = submit2Id,
                    AssignedToUserId = dev3Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Fight Zabuza"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Fight Zabuza",
                    Created = DateTime.Parse("8/8/2018 9:40:00 AM"),
                    ProjectId = Project2.Id,
                    TicketTypeId = ticketType1Id,
                    TicketPriorityId = ticketPriority1Id,
                    TicketStatusId = ticketStatus1Id,
                    SubmittedByUserId = submit2Id,
                    AssignedToUserId = dev5Id

                });
            }
            if (!context.Tickets.Any(t => t.Title == "Avenge the Uchiha Clan"))
            {
                context.Tickets.Add(new Ticket
                {
                    Title = "Avenge the Uchiha Clan",
                    Created = DateTime.Parse("8/4/2011 6:13:23 PM"),
                    ProjectId = Project2.Id,
                    TicketTypeId = ticketType2Id,
                    TicketPriorityId = ticketPriority4Id,
                    TicketStatusId = ticketStatus2Id,
                    SubmittedByUserId = submit2Id,
                    AssignedToUserId = dev5Id

                });
            }

            #endregion


            #endregion

            #endregion
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
