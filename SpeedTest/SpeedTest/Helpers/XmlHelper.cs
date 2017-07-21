using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SpeedTest.Helpers
{
    public class XmlHelper
    {
        public static Urlset Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(Urlset));
            Urlset result;

            using (TextReader reader = new StringReader(xml))
            {
                result = (Urlset)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}