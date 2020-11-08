using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HuePipeWorkerService.DTOs.Hue;
using HuePipeWorkerService.Services;
using HuePipeWorkerService.Services.GitLab;
using HuePipeWorkerService.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HuePipeWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Configuration _config;
        private IPipelineService _pipelineService;
        private IHueService _hueService;
        private int lastPipelineId = 0;
        public Worker(ILogger<Worker> logger, IPipelineService pipelineService, IHueService hueService, IOptions<Configuration> config)
        {
            _logger = logger;
            _pipelineService = pipelineService;
            _hueService = hueService;
            _config = config.Value;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                runTask();
                await Task.Delay(100000, stoppingToken);
            }
        }

        private void runTask()
        {
            var pipelines = _pipelineService.getPipelines(_config.ProjectId);
            var lastPipeline = pipelines.FirstOrDefault();
            
            if (lastPipeline.id > lastPipelineId)
            {         
                lastPipelineId = lastPipeline.id;
                _logger.LogInformation("I found a new pipeline id: {id}", lastPipelineId);

                Light light = new Light();

                if (pipelines.FirstOrDefault().status == Enums.PipelineStatus.failed.ToString())
                {
                   light = new Light
                    {
                        id = 1,
                        name = string.Empty,
                        state =
                        new State
                        {
                            on = true,
                            bri = 254,
                            hue = 65373,
                            sat = 249,
                            xy = new double[] { 0.6807, 0.3074 },
                            ct = 153
                        }
                    };    
                }
                else if (pipelines.FirstOrDefault().status == Enums.PipelineStatus.success.ToString())
                {

                   light = new Light
                    {
                        id = 1,
                        name = string.Empty,
                        state =
                        new State
                        {
                            on = true,
                            bri = 254,
                            hue = 27908,
                            sat = 170,
                            xy = new double[] { 0.2384, 0.5462 },
                            ct = 153
                        }
                    };

                }

                try
                {
                    _hueService.setLight(light, light.state);
                    _logger.LogInformation("I changed the color!");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception: {message}", ex.ToString());
                }
            }
        }
    }
}
