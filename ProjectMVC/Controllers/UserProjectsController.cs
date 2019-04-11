using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
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

        // GET: UserProjects
        public async Task<ActionResult> Index(int? projectId)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name);

            Logica.BL.Tenants tenants = new Logica.BL.Tenants();
            var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

            Logica.BL.AspNetUsers aspNetUsers = new Logica.BL.AspNetUsers();
            var listUsers = aspNetUsers.GetUsers(tenant.Id);

            Logica.BL.UserProjects userProjects = new Logica.BL.UserProjects();
            var listUsersProject = userProjects.GetUsersProject(projectId);

            var listUsersViewModel = (from _users in listUsers
                                      where !listUsersProject.Select(x => x.UserId).Contains(_users.Id)
                                      select new Logica.Models.ViewModels.Users
                                      {
                                          Id = _users.Id,
                                          Email = _users.Email,
                                          UserName = _users.UserName
                                      }).ToList();

            var listUsersProjectViewModel = (from _users in listUsersProject
                                             select new Logica.Models.ViewModels.Users
                                             {
                                                 Id = _users.UserId,
                                                 Email = _users.AspNetUsers.Email,
                                                 UserName = _users.AspNetUsers.UserName
                                             }).ToList();

            return View(new Logica.Models.ViewModels.UserProjectsIndexViewModel
            {
                Users = listUsersViewModel,
                UsersProject = listUsersProjectViewModel
            });
        }
    }
}