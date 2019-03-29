using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.Logica.Models.ViewModels
{
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
}
