using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpeedTest.Models
{
    public class Measurement
    {
        public Measurement()
        {
            MeasuredUrls = new List<MeasuredUrl>();
        }

        public int Id { get; set; }

        public DateTime DateOfMeasuring { get; set; }

        public virtual ICollection<MeasuredUrl> MeasuredUrls { get; set; }
        
        public virtual Site Site { get; set; }
    }
}