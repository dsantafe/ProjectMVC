using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        // GET: Tasks
        public ActionResult Index(int? projectId)
        {
            Logica.BL.Tasks tasks = new Logica.BL.Tasks();
            var listTasks = tasks.GetTasks(projectId, null);

            var listTasksViewModel = listTasks.Select(x => new Logica.Models.ViewModels.TasksIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Details = x.Details,
                ExpirationDate = x.ExpirationDate,
                IsCompleted = x.IsCompleted,
                Effort = x.Effort,
                RemainingWork = x.RemainingWork,
                State = x.States.Name,
                Activity = x.Activities.Name,
                Priority = x.Priorities.Name
            }).ToList();

            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null).FirstOrDefault();

            ViewBag.Project = project;

            return View(listTasksViewModel);
        }

        public ActionResult Create(int? projectId)
        {
            var taskBindingModel = new Logica.Models.BindingModels.TasksCreateBindingModel
            {
                ProjectId = projectId
            };

            Logica.BL.States states = new Logica.BL.States();
            ViewBag.States = states.GetStates();

            Logica.BL.Activities activities = new Logica.BL.Activities();
            ViewBag.Activities = activities.GetActivities();

            Logica.BL.Priorities priorities = new Logica.BL.Priorities();
            ViewBag.Priorities = priorities.GetPriorities();

            return View(taskBindingModel);
        }

        [HttpPost]
        public ActionResult Create(Logica.Models.BindingModels.TasksCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                tasks.CreateTasks(model.Title,
                    model.Details,
                    model.ExpirationDate,
                    model.IsCompleted,
                    model.Effort,
                    model.RemainingWork,
                    model.StateId,
                    model.ActivityId,
                    model.PriorityId,
                    model.ProjectId);

                return RedirectToAction("Index", new { projectId = model.ProjectId });
            }

            Logica.BL.States states = new Logica.BL.States();
            ViewBag.States = states.GetStates();

            Logica.BL.Activities activities = new Logica.BL.Activities();
            ViewBag.Activities = activities.GetActivities();

            Logica.BL.Priorities priorities = new Logica.BL.Priorities();
            ViewBag.Priorities = priorities.GetPriorities();

            return View(model);
        }

        public ActionResult Calendar(int? projectId)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null).FirstOrDefault();

            ViewBag.Project = project;

            return View();
        }

        public ActionResult GetTasksCalendar(int? projectId)
        {
            try
            {
                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                var listTasks = tasks.GetTasks(projectId, null);

                var listTasksCalendarViewModel = listTasks.Select(x => new Logica.Models.ViewModels.TasksGetTasksCalendarViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Start = x.ExpirationDate.Value.AddDays(Convert.ToDouble(-x.RemainingWork)).ToString("yyyy-MM-dd HH:mm:ss"),
                    End = x.ExpirationDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    AllDay = false,
                    Color = "#0027FF",
                    TextColor = "#000000"
                }).ToList();

                return Json(new
                {
                    Data = listTasksCalendarViewModel,
                    IsSuccessful = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModels.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                },
                JsonRequestBehavior.AllowGet);
            }
        }
    }
}