using HuePipeWorkerService.DTOs.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuePipeWorkerService.Services
{
    public interface IPipelineService
    {
        public List<Project> getProjects();
        public List<Pipeline> getPipelines(int projectId);

    }
}
