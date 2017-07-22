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

            string xml = await HttpRequestHelper.GetResponseString("https://dou.ua/sitemap-forums.xml");

            var urls = XmlHelper.Deserialize(xml).Urls;

            var listOfUrls = new List<string>();

            foreach(var url in urls.Take(4000))
            {
                listOfUrls.Add(url.Loc);
            }

            sw.Start();
            await RequestFactory.MeasureInParallel(listOfUrls);
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
