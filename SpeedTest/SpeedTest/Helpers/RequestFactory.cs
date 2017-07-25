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
    public class RequestFactory : IRequestFactory
    {
        private ConcurrentBag<MeasuredUrl> bag = new ConcurrentBag<MeasuredUrl>();

        public async Task<IEnumerable<MeasuredUrl>> ConcurrentMeasure(IEnumerable<Url> urls)
        {
            var tasks = new List<Task>();

            foreach (var url in urls)
            {
                tasks.Add(GetUrl(url));
            }

            await Task.WhenAll(tasks);

            return bag;
        }

        private async Task GetUrl(Url url)
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