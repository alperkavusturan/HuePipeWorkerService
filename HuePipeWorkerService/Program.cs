using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuePipeWorkerService.Services;
using HuePipeWorkerService.Services.GitLab;
using HuePipeWorkerService.Services.Hue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GitLab = HuePipeWorkerService.Services.GitLab;
using Hue = HuePipeWorkerService.Services.Hue;

namespace HuePipeWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<GitLab.Configuration>(hostContext.Configuration.GetSection("GitLab"));
                    services.Configure<Hue.Configuration>(hostContext.Configuration.GetSection("Hue"));
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IHueService, HueService>();
                    services.AddSingleton<IPipelineService, GitLabService>();
                });
    }
}
