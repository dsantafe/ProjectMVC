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
    [Authorize]
    public class ProjectsJsonController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set { _userManager = value; }
        }

        public ProjectsJsonController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ProjectsJsonController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Projects
        public async Task<ActionResult> GetProjects()
        {
            try
            {
                ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name);

                Logica.BL.Tenants tenants = new Logica.BL.Tenants();
                var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

                Logica.BL.Projects projects = new Logica.BL.Projects();

                var result = await UserManager.IsInRoleAsync(user.Id, "Admin") ?
                    projects.GetProjects(null, tenant.Id) :
                    projects.GetProjects(null, tenant.Id, user.Id);

                var listProjects = result.Select(x => new Logica.Models.ViewModels.ProjectsIndexViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Details = x.Details,
                    ExpectedCompletionDateString = x.ExpectedCompletionDate == null ? string.Empty : x.ExpectedCompletionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedAtString = x.CreatedAt == null ? string.Empty : x.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAtString = x.UpdatedAt == null ? string.Empty : x.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

                listProjects = tenant.Plan.Equals("Premium") ?
                    listProjects :
                    listProjects.Take(1).ToList();

                return Json(new
                {
                    Data = listProjects,
                    IsSuccessful = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModels.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Logica.Models.BindingModels.ProjectsCreateBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name);

                    Logica.BL.Tenants tenants = new Logica.BL.Tenants();
                    var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

                    Logica.BL.Projects projects = new Logica.BL.Projects();
                    projects.CreateProjects(model.Title,
                        model.Details,
                        model.ExpectedCompletionDate,
                        tenant.Id);
                }
                else
                {
                    return Json(new Logica.Models.ViewModels.ResponseViewModel
                    {
                        IsSuccessful = false,
                        Errors = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    IsSuccessful = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModels.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(id, null).FirstOrDefault();

            var projectBindingModel = new Logica.Models.BindingModels.ProjectsEditBindingModel
            {
                Id = project.Id,
                Details = project.Details,
                ExpectedCompletionDate = project.ExpectedCompletionDate,
                Title = project.Title
            };

            return View(projectBindingModel);
        }

        [HttpPost]
        public ActionResult Edit(Logica.Models.BindingModels.ProjectsEditBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Logica.BL.Projects projects = new Logica.BL.Projects();
                    projects.UpdateProjects(model.Id,
                        model.Title,
                        model.Details,
                        model.ExpectedCompletionDate);
                }
                else
                {
                    return Json(new Logica.Models.ViewModels.ResponseViewModel
                    {
                        IsSuccessful = false,
                        Errors = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    IsSuccessful = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModels.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int? id)
        {

            ViewBag.ProjectId = id;

            return View();
        }

        public ActionResult GetProject(int? id)
        {
            try
            {
                Logica.BL.Projects projects = new Logica.BL.Projects();
                var project = projects.GetProjects(id, null).FirstOrDefault();

                var projectDetailsViewModel = new Logica.Models.ViewModels.ProjectsDetailsViewModel
                {
                    Details = project.Details,
                    ExpectedCompletionDateString = project.ExpectedCompletionDate == null ? string.Empty : project.ExpectedCompletionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    Title = project.Title,
                    CreatedAtString = project.CreatedAt == null ? string.Empty : project.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAtString = project.UpdatedAt == null ? string.Empty : project.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return Json(new
                {
                    IsSuccessful = true,
                    Data = projectDetailsViewModel
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModels.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            projects.DeleteProjects(id);

            return RedirectToAction("Index");
        }
    }
}