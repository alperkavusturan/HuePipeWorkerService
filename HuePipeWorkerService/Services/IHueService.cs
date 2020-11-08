using HuePipeWorkerService.DTOs.Hue;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuePipeWorkerService.Services
{
    public interface IHueService
    {
        public List<Light> getLights();
        public bool setLight(Light light, State state);
    }
}
