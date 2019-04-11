using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectMVC.Models
{
    public class ResponseViewModel
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}