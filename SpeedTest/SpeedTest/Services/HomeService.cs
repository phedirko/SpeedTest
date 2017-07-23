using SpeedTest.Data;
using SpeedTest.Data.Models;
using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SpeedTest.Services
{
    public static class HomeService
    {
        private static ISiteRepository _siterepo = new SiteRepository(new STdbcontext());

        public static async Task<IEnumerable<MeasuredUrl>> ProcessRequest(string siteUrl)
        {
            if (!siteUrl.StartsWith("http://"))
                siteUrl = "http://" + siteUrl;

            Site site = new Site
            {
                Address = siteUrl,
            };
           
            var m = new Measurement();
            
            var sitemapXml = await Helpers.HttpRequestHelper.GetResponseString(site.SitemapAddress);
            var urlset = Helpers.XmlHelper.Deserialize(sitemapXml);

            var urlsToMeasure = GetUrls(GetLocsFromUrlset(urlset)).ToList();
            ((List<Url>)site.Urls).AddRange(urlsToMeasure);

            var measuredUrls = await Helpers.RequestFactory.ConcurrentMeasure(site.Urls);

            m.MeasuredUrls = measuredUrls.ToList();
            m.DateOfMeasuring = DateTime.Now;
            site.Measurements.Add(m);


            try
            {

                _siterepo.InsertSite(site);
                _siterepo.Save();
            } catch(Exception ex)
            {

            }

            return m.MeasuredUrls;

            
        }

        private static IEnumerable<string> GetLocsFromUrlset(Urlset urlset)
        {
            return urlset.Urls.Select(x => x.Loc);
        }

        private static IEnumerable<Url> GetUrls(IEnumerable<string> urls)
        {
            foreach(var url in urls)
            {
                yield return new Url { Location = url };
            }
        }
    }
}