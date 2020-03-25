using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KaZe.Helpers;
using KaZe.Models;
using KaZe.View_Models;
using Microsoft.AspNet.Identity;

namespace KaZe
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper roleHelper = new RolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        public ActionResult ManageUsers()
        {
            var managers = roleHelper.UsersInRole("Project Manager");
            ViewBag.ProjManId = new SelectList(managers, "Id", "Email");

            var developers = roleHelper.UsersInRole("Developer");
            ViewBag.DevId = new SelectList(developers, "Id", "Email");

            var submitters = roleHelper.UsersInRole("Submitter");
            ViewBag.SubmitId = new SelectList(submitters, "Id", "Email");

            ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUsers(string projManId, List<string> devIds, List<string> submitIds, List<int> projectIds)
        {
            if (projectIds != null)
            {
                if (!string.IsNullOrEmpty(projManId))
                {
                    foreach (var projId in projectIds)
                    {
                        if (!projectHelper.IsUserOnProject(projManId, projId))
                        {
                            projectHelper.AddUserToProject(projManId, projId);
                        }else if (projectHelper.IsUserOnProject(projManId, projId))
                        {
                            projectHelper.RemoveUserFromProject(projManId, projId);
                        }
                    }
                }


                if (devIds != null)
                {
                    foreach (var dev in devIds)
                    {
                        foreach (var projId in projectIds)
                        {
                            if (!projectHelper.IsUserOnProject(dev, projId))
                            {
                                projectHelper.AddUserToProject(dev, projId);
                            }
                            else if (projectHelper.IsUserOnProject(dev, projId))
                            {
                                projectHelper.RemoveUserFromProject(dev, projId);
                            }
                        }
                    }
                }
                if (submitIds != null)
                {
                    foreach (var subId in submitIds)
                    {
                        foreach (var projId in projectIds)
                        {
                            if (!projectHelper.IsUserOnProject(subId, projId))
                            {
                                projectHelper.AddUserToProject(subId, projId);
                            }
                            else if(projectHelper.IsUserOnProject(subId, projId))
                                {
                                    projectHelper.AddUserToProject(subId, projId);
                                }
                        }
                    }
                }
            }
                return RedirectToAction("Index", "Home");
        }
        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            var model = new ProjectViewModel();
            var userId = User.Identity.GetUserId();
            model.projectManager = projectHelper.WhoIsProjMan(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.project = db.Projects.Find(id);

            if (model.project == null)
            {
                return HttpNotFound();
            }
            model.personnelDev = projectHelper.UsersOnProject(model.project.Id);
            model.personnelDev.Remove(projectHelper.WhoIsProjMan(model.project.Id));
            return View(model);
        }
        [Authorize(Roles = "Global Admin,Admin, Demo Admin, Project Manager")]
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles= "Global Admin, Admin, Project Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
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
