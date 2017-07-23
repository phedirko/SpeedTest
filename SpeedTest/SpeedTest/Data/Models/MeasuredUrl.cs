using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTest.Models
{
    public class MeasuredUrl
    {
        public MeasuredUrl() { }
        
        public int Id { get; set; }

        public string Url { get; set; }

        public TimeSpan ElapsedTime { get; set; }
        
        public virtual Measurement Measurement { get; set; }
    }
}