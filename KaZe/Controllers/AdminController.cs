using KaZe.Helpers;
using KaZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KaZe.Controllers
{
    [Authorize(Roles = "Admin, Global Admin, Demo Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper rolesHelper = new RolesHelper();
        // GET: Admin
        public ActionResult ManageRole()
        {
            var assignableRoles =db.Roles.Where(r=>r.Name != "Global Admin");
            ViewBag.Roles = new SelectList(assignableRoles, "Name", "Name");

            var allUsers = db.Users.ToList();
            ViewBag.AllUsers = new MultiSelectList(allUsers, "Id", "Email");


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRole(List<string> AllUsers, string Roles)
        {
            if (AllUsers != null)
            {
                foreach (var userId in AllUsers)
                {
                    var allRolesForSected = rolesHelper.ListUserRoles(userId);
                    foreach (var role in allRolesForSected)
                    {
                        rolesHelper.RemoveUserFromRole(userId, role);
                    }
                    if (!string.IsNullOrEmpty(Roles))
                    {
                        rolesHelper.AddUserToRole(userId, Roles);
                    }
                }

            }


            return RedirectToAction("Index", "Home");
        }
    }
}