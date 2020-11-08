using System;
using System.Collections.Generic;
using System.Text;

namespace HuePipeWorkerService.DTOs.Pipeline
{
    public class Pipeline
    {
        public int id { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }
}
