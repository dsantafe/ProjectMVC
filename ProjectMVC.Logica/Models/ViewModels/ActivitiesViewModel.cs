using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.Logica.Models.ViewModels
{
    public class ActivitiesIndexViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Active")]
        public bool? Active { get; set; }
    }
}
