using System.Collections.Generic;

namespace ProjectMVC.Logica.Models.ViewModels
{
    public class ResponseViewModel
    {
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
    }
}
