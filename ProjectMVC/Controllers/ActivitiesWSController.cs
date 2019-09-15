using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class ActivitiesWSController : Controller
    {
        // GET: Activities
        public ActionResult Index()
        {
            Test.Test test = new Test.Test();
            test.Url = ConfigurationManager.AppSettings["WebServiceEndpoint"].ToString();

            var listActivities = (from q in test.GetActivities()
                                  select new Logica.Models.ViewModels.ActivitiesIndexViewModel
                                  {
                                      Id = q.Id,
                                      Name = q.Name,
                                      Active = q.Active
                                  }).ToList();

            return View(listActivities);
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Logica.Models.BindingModels.ActivitiesCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Test.Test test = new Test.Test();
                test.Url = ConfigurationManager.AppSettings["WebServiceEndpoint"].ToString();

                Logica.BL.Utils utils = new Logica.BL.Utils();
                var nameEncode = utils.Base64Encode(model.Name);

                test.CreateActivity(nameEncode);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Test.Test test = new Test.Test();
            test.Url = ConfigurationManager.AppSettings["WebServiceEndpoint"].ToString();

            var activity = (from q in test.GetActivities()
                            where q.Id == id
                            select new Logica.Models.BindingModels.ActivitiesEditBindingModel
                            {
                                Id = q.Id,
                                Name = q.Name,
                                Active = q.Active
                            }).ToList();

            return View(activity);
        }

        // POST: Activities/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Edit(Logica.Models.BindingModels.ActivitiesEditBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Test.Test test = new Test.Test();
                test.Url = ConfigurationManager.AppSettings["WebServiceEndpoint"].ToString();

                Logica.BL.Utils utils = new Logica.BL.Utils();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Activities/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Test.Test test = new Test.Test();
            test.Url = ConfigurationManager.AppSettings["WebServiceEndpoint"].ToString();            

            return RedirectToAction("Index");
        }
    }
}
