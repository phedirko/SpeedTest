using SpeedTest.Data;
using SpeedTest.Models;
using System;
using System.Collections.Concurrent;
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
        private static ConcurrentBag<MeasuredUrl> bag = new ConcurrentBag<MeasuredUrl>();

        public static async Task<IEnumerable<MeasuredUrl>> ConcurrentMeasure(IEnumerable<Url> urls)
        {
            bag = null;

            bag = new ConcurrentBag<MeasuredUrl>();

            var tasks = new List<Task>();

            foreach (var url in urls)
            {
                tasks.Add(GetUrl(url));
            }

            await Task.WhenAll(tasks);

            return bag;
        }

        private static async Task GetUrl(Url url)
        {
            var sw = new Stopwatch();

            using (var client = new HttpClient())
            {
                sw.Start();
                try
                {
                    await client.GetAsync(url.Location);
                }
                catch
                {
                }
                sw.Stop();
                bag.Add(new MeasuredUrl { Url = url.Location, ElapsedTime = sw.Elapsed });

            }
        }
    }
}