using HuePipeWorkerService.DTOs.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace HuePipeWorkerService.Services.GitLab
{
    public class GitLabService : IPipelineService
    {
        public string Endpoint { get; set; }
        private readonly Configuration _config;
        public RestClient client { get; set; }

        public GitLabService(IOptions<Configuration> config)
        {
            _config = config.Value;
            Endpoint = _config.ServiceUrl;

            client = new RestClient(Endpoint);
        }

        public List<Project> getProjects()
        {
            var request = new RestRequest(Endpoint + "/projects?private_token=" + _config.Token, Method.GET);
            var response = client.Execute(request);

            return JsonSerializer.Deserialize<List<Project>>(response.Content);
        }

        public List<Pipeline> getPipelines(int projectId)
        {
            var request = new RestRequest(Endpoint + $"/projects/{projectId}/pipelines?private_token=" + _config.Token, Method.GET);
            var response = client.Execute(request);

            return JsonSerializer.Deserialize<List<Pipeline>>(response.Content);
        }
    }
}
