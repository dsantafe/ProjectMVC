using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class UserProjectsController : Controller
    {
        // GET: UserProjects
        public ActionResult Index()
        {
            var listUsers = new List<Logica.Models.ViewModels.UserProjectsIndexViewModel> {
                new Logica.Models.ViewModels.UserProjectsIndexViewModel{
                    Email = "dsantafe@navisaf.com",
                    UserName = "dsantafe@navisaf.com",
                    Id = "123"
                }
            };

            var listUsersProject = new List<Logica.Models.ViewModels.UserProjectsIndexViewModel> {
                new Logica.Models.ViewModels.UserProjectsIndexViewModel{
                    Email = "lvaldes@navisaf.com",
                    UserName = "lvaldes@navisaf.com",
                    Id = "123"
                }
            };

            ViewBag.ListUsers = listUsers;
            ViewBag.ListUsersProject = listUsersProject;

            return View();
        }
    }
}