using System;
using System.Collections.Generic;
using System.Text;

namespace HuePipeWorkerService.DTOs.Hue
{
    public class Light
    {
        public int id { get; set; }
        public State state { get; set; }
        public string name { get; set; }

    }

    public class State 
    {
        public bool on { get; set; }
        public double bri { get; set; }
        public double hue { get; set; }
        public double sat { get; set; }
        public double[] xy { get; set; }
        public int ct { get; set; }
    }
}
