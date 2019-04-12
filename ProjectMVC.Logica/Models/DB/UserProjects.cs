using System;

namespace ProjectMVC.Logica.Models.DB
{
    public class UserProjects
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public AspNetUsers AspNetUsers { get; set; }
        public Projects Projects { get; set; }
    }
}
