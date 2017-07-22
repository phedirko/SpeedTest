using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SpeedTest.Helpers
{
    public static class RequestFactory
    {
        static int i = 0;

        public static async Task MeasureInParallel(List<string> urls)
        { 
            var tasks = new List<Task<TimeSpan>>();

            foreach(var url in urls)
            {
                tasks.Add(GetUrl(url));
            }

            await Task.WhenAll(tasks);
        }


        private static async Task<TimeSpan> GetUrl(string url)
        {
            var sw = new Stopwatch();

            using (var client = new HttpClient())
            {
                sw.Start();
                try
                {
                    await client.GetAsync(url);
                }
                catch (Exception x)
                {
                }
                sw.Stop();
            }

            Interlocked.Increment(ref i);    

            Console.WriteLine($"{url.Replace("http://", "")} : {sw.Elapsed.Milliseconds} i: {i}");
            Console.WriteLine();

            return sw.Elapsed;
        }
    }
}