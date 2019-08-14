using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.Logica.Models.ViewModels
{
    public class ProjectsIndexViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "ExpectedCompletionDate")]
        public DateTime? ExpectedCompletionDate { get; set; }

        [Display(Name = "ExpectedCompletionDate")]
        public string ExpectedCompletionDateString { get; set; }

        [Display(Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "CreatedAt")]
        public string CreatedAtString { get; set; }

        [Display(Name = "UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "UpdatedAt")]
        public string UpdatedAtString { get; set; }
    }

    public class ProjectsDetailsViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "ExpectedCompletionDate")]
        public DateTime? ExpectedCompletionDate { get; set; }

        [Display(Name = "ExpectedCompletionDate")]
        public string ExpectedCompletionDateString { get; set; }

        [Display(Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "CreatedAt")]
        public string CreatedAtString { get; set; }

        [Display(Name = "UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "UpdatedAt")]
        public string UpdatedAtString { get; set; }
    }
}
