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
            var project = 
                projects.GetProjects(projectId, null).FirstOrDefault();

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

        private readonly Dictionary<int, string> ColorState = new Dictionary<int, string> {
            { 1, "#FFFF00" },
            { 2, "#FF8000" },
            { 3, "#088A08" }
        };

        public ActionResult Calendar(int? projectId)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null).FirstOrDefault();

            ViewBag.Project = project;
            return View();
        }

        [HttpPost]
        public ActionResult CalendarJSON(int? projectId)
        {
            try
            {
                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                var listTasks = tasks.GetTasks(projectId, null);                

                var listTasksCalendarViewModel = listTasks.Select(x => new Logica.Models.ViewModels.TasksCalendarViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    AllDay = true,
                    Color = ColorState.FirstOrDefault(y => y.Key == x.StateId).Value,
                    Start = x.ExpirationDate.Value.AddDays(Convert.ToDouble(-x.RemainingWork)).ToString("yyyy-MM-dd HH:mm:ss"),
                    End = x.ExpirationDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    TextColor = "#000000"
                }).ToList();
                
                var json = Json(new
                {
                    ListTasksCalendar = listTasksCalendarViewModel,
                    IsSuccessful = true
                });

                return json;
            }
            catch (Exception ex)
            {
                return Json(new Models.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        public ActionResult GetTasksJSON(int? id)
        {
            try
            {
                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                var listTasks = tasks.GetTasks(null, id);

                var taskViewModel = listTasks.Select(x => new Logica.Models.ViewModels.TasksDetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Details = x.Details,
                    ExpirationDate = x.ExpirationDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    Effort = x.Effort,
                    RemainingWork = x.RemainingWork,
                    State = x.States.Name,
                    Activity = x.Activities.Name,
                    Priority = x.Priorities.Name
                }).FirstOrDefault();

                var json = Json(new
                {
                    Task = taskViewModel,
                    IsSuccessful = true
                });

                return json;                
            }
            catch (Exception ex)
            {
                return Json(new Models.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}