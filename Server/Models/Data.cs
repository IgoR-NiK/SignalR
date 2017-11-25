using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Server.Models
{
    public class Data
    {
        [JsonProperty("startX")]
        public float StartX { get; set; }

        [JsonProperty("startY")]
        public float StartY { get; set; }

        [JsonProperty("endX")]
        public float EndX { get; set; }

        [JsonProperty("endY")]
        public float EndY { get; set; }

        [JsonProperty("colorPen")]
        public string ColorPen { get; set; }

        [JsonProperty("timeDelta")]
        public int TimeDelta { get; set; }
    }
}