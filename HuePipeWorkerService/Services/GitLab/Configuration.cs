using System;
using System.Collections.Generic;
using System.Text;

namespace HuePipeWorkerService.Services.GitLab
{
    public class Configuration
    {
        public string ServiceUrl { get; set; }
        public string Token { get; set; }
        public int ProjectId { get; set; }
    }
}
