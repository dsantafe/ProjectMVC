using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class MembersController : Controller
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

        public MembersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public MembersController()
        {
                
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Logica.Models.BindingModels.MembersCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userInSession = await UserManager.FindByNameAsync(User.Identity.Name);

                Logica.BL.Tenants tenants = new Logica.BL.Tenants();
                var tenant = tenants.GetTenants(userInSession.Id).FirstOrDefault();

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                var result = await UserManager.CreateAsync(user, "Qwer1234*");

                if (result.Succeeded)
                {
                    ViewBag.Message = "This invitation was intended for " + model.Email + " is successful";
                    ViewBag.Success = true;

                    var _context = new DAL.Models.ProjectMVCEntities();

                    var aspNetUser = _context.AspNetUsers.Where(x => x.UserName.Equals(model.Email)).FirstOrDefault();
                    aspNetUser.TenantId = tenant.Id;
                    _context.SaveChanges();

                    var userResult = await UserManager.FindByNameAsync(model.Email);
                    if (userResult != null)
                    {
                        await UserManager.AddToRoleAsync(userResult.Id, "Member");

                        var code = await UserManager.GeneratePasswordResetTokenAsync(userResult.Id);
                        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = userResult.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(userResult.Id,
                            userInSession.UserName + " invited you to organization",
                            userInSession.UserName + " has invited you to collaborate on the organization: <a href=\"" + callbackUrl + "\">View invitation</a>");

                        return View();
                    }
                }

                ViewBag.Message = "An error has occurred during the process";
            }

            return View(model);
        }
    }
}