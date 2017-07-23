using SpeedTest.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                Task.Run(MainAsync).Wait();
            }
            catch (Exception ex)
            {

            }
            Console.Read();
        }

        static async Task MainAsync()
        {
            var sw = new Stopwatch();

            string xml = await HttpRequestHelper.GetResponseString("http://gymnasium152.edu.kh.ua/sitemap.xml");

            var urls = XmlHelper.Deserialize(xml).Urls;

            var listOfUrls = new List<string>();

            urls = urls.ToList();

            foreach (var url in urls)
            {
                listOfUrls.Add(url.Loc);
            }



            
        }
    }
}
