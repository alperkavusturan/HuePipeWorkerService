using System;
using System.Collections.Generic;
using System.Text;

namespace HuePipeWorkerService.DTOs.Pipeline
{
    public class Project
    {
        public int id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
    }
}
