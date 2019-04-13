using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class UserProjectsController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UserProjectsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public UserProjectsController()
        {

        }

        // GET: UserProjects
        public async Task<ActionResult> Index(int? projectId)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name);

            Logica.BL.Tenants tenants = new Logica.BL.Tenants();
            var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null, null).FirstOrDefault();

            Logica.BL.AspNetUsers aspNetUsers = new Logica.BL.AspNetUsers();
            var listUsers = aspNetUsers.GetUsers(tenant.Id);

            Logica.BL.UserProjects userProjects = new Logica.BL.UserProjects();
            var listUsersProject = userProjects.GetUsersProject(projectId);

            var listUsersResult = (from _users in listUsers
                                   where !listUsersProject.Select(x => x.UserId).Contains(_users.Id)
                                   select new Logica.Models.ViewModels.Users
                                   {
                                       Id = _users.Id,
                                       Email = _users.Email,
                                       UserName = _users.UserName
                                   }).ToList();

            var listUsersProjectResult = listUsersProject.Select(x => new Logica.Models.ViewModels.Users
            {
                Id = x.UserId,
                Email = x.AspNetUsers.Email,
                UserName = x.AspNetUsers.UserName
            }).ToList();

            var listUserProjectsIndexViewModel = new Logica.Models.ViewModels.UserProjectsIndexViewModel
            {
                Users = listUsersResult,
                UsersProject = listUsersProjectResult
            };

            ViewBag.Project = project;

            return View(listUserProjectsIndexViewModel);
        }

        public ActionResult AddUser(int? projectId, string userId)
        {
            return RedirectToAction("Index", new { projectId });
        }

        public ActionResult RemoveUser(int? projectId, string userId)
        {
            return RedirectToAction("Index", new { projectId });
        }
    }
}