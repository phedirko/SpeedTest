﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpeedTest.Models
{
    public class Site
    {
        public int Id { get; set; }

        public string Address { get; set; }

        [NotMapped]
        public string SitemapAddress
        {
            get
            {
                return Address + "/sitemap.xml";
            }
        }

        public virtual ICollection<Url> Urls { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}