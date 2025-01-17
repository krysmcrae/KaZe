﻿using KaZe.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaZe.Helpers
{

    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static RolesHelper roleHelper = new RolesHelper();
        public static string RetrieveDisplayName()
        {
            var userId =HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
            {
                return "Demo User";
            }
            var user = db.Users.Find(userId);
            return (user.FullName);
        }
        public static ICollection<Project> RetrieveAllProjects()
        {
            var projects = db.Projects.ToList();
            return projects;
        }
        public static string RetrieveRole()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
            {
                return "Demo Admin";
            }
            
            var role = roleHelper.ListUserRoles(userId).FirstOrDefault();
            return (role);
        }
    }
}