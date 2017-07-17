using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTest.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        
        public IEnumerable<Url> Urls { get; set; }

        public DateTime DateOfMeasuring { get; set; }
    }
}