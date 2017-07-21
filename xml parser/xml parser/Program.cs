using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace xml_parser
{
    [XmlRoot(ElementName = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Url
    {
        [XmlElement(ElementName = "loc", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Loc { get; set; }
        [XmlElement(ElementName = "lastmod", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Lastmod { get; set; }
    }

    [XmlRoot(ElementName = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Urlset
    {
        [XmlElement(ElementName = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public List<Url> Url { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "xhtml", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xhtml { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string xml =
                "<urlset><url><loc>https://mycredit.ua/</loc><lastmod>2017-07-20</lastmod><changefreq>daily</changefreq><priority>1</priority></url><url><loc>https://mycredit.ua/ru/oformit-kredit/</loc><lastmod>2017-07-20</lastmod><changefreq>daily</changefreq><priority>0.8</priority></url></urlset>";

            xml = GetResponseString("").Result;

            var x = Deserialize(xml);
        }

        public static async Task<string> GetResponseString(string text)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://mycredit.ua/sitemap.xml");
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }

        public static Urlset Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(Urlset));
            Urlset result;

            using (TextReader reader = new StringReader(xml))
            {
                result = (Urlset)serializer.Deserialize(reader);
            }
            
            return null;
        }
    }


}
