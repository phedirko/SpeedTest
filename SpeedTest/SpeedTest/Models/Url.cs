using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTest.Models
{
    public class Url
    {
        public int Id { get; set; }

        public string Location { get; set;}

        public TimeSpan Elapsed { get; set; } 
    }
}