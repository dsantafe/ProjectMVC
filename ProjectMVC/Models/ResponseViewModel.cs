using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectMVC.Models
{
    public class ResponseViewModel
    {
        public bool IsSuccessful { get; set; }
        
        public List<string> Errors { get; set; }
    }
}