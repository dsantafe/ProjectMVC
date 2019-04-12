using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.Logica.Models.ViewModels
{
    public class Users
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }

    public class UserProjectsIndexViewModel
    {
        public List<Users> Users { get; set; }
        public List<Users> UsersProject { get; set; }
    }
}
