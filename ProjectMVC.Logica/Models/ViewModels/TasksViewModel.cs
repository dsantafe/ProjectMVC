using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProjectMVC.Logica.Models.ViewModels
{
    #region TasksIndexViewModel
    public class TasksIndexViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "IsCompleted")]
        public bool? IsCompleted { get; set; }

        [Display(Name = "Effort")]
        public int? Effort { get; set; }

        [Display(Name = "RemainingWork")]
        public int? RemainingWork { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }
    }
    #endregion

    #region TasksCalendarViewModel
    public class TasksCalendarViewModel
    {
        public int Id { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public bool AllDay { get; set; }

        public string TextColor { get; set; }
    }
    #endregion

    #region TasksDetailsViewModel
    public class TasksDetailsViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "ExpirationDate")]
        public string ExpirationDate { get; set; }        

        [Display(Name = "Effort")]
        public int? Effort { get; set; }

        [Display(Name = "RemainingWork")]
        public int? RemainingWork { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }
    }
    #endregion
}