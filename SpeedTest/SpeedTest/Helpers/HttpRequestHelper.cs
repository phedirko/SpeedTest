using System;
using System.Collections.Generic;
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
    }
}