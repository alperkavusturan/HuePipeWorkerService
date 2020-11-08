using HuePipeWorkerService.DTOs.Hue;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace HuePipeWorkerService.Services.Hue
{
    public class HueService : IHueService
    {
        public string Endpoint { get; set; }
        private readonly Configuration _config;
        public RestClient client { get; set; }

        public HueService(IOptions<Configuration> config)
        {
            _config = config.Value;
            Endpoint = _config.ServiceUrl;

            client = new RestClient(Endpoint);
        }
        public List<Light> getLights()
        {
            var request = new RestRequest($"/{_config.Token}/lights", Method.GET);

            try
            {

                var response = client.Execute(request);
                return JsonSerializer.Deserialize<List<Light>>(response.Content);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool setLight(Light light, State state)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var request = new RestRequest($"/{_config.Token}/lights/{light.id}/state", Method.PUT);
            request.AddJsonBody(state);
            var response = client.Execute(request);

              return response.IsSuccessful;
        }
    }
}
