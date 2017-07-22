using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SpeedTest.Helpers
{
    public static class HttpRequestHelper
    {
        public static async Task<string> GetResponseString(string sitemapUrl)
        {
            string responseResult;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(sitemapUrl);
                responseResult = await response.Content.ReadAsStringAsync();
            }

            return responseResult;
        }

        public static async Task<TimeSpan> MeasureResponseTime(string url)
        {
            var sw = new Stopwatch();
            using(var client = new HttpClient())
            {
                sw.Start();
                await client.GetAsync(url);
                sw.Stop();
            }

            return sw.Elapsed;
        }
    }
}