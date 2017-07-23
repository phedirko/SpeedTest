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
            string siteAddr = siteUrl;

            if (!siteUrl.StartsWith("http://"))
                siteUrl = "http://" + siteUrl;



            Site site = _siterepo.GetSiteByUrl(siteUrl);
            Measurement m;
            if(site == null)
            {
                site = new Site
                {
                    Address = siteUrl,
                   
                };

                var sitemapXml = await Helpers.HttpRequestHelper.GetResponseString(site.SitemapAddress);
                var urlset = Helpers.XmlHelper.Deserialize(sitemapXml);

                var urlsToMeasure = GetUrls(GetLocsFromUrlset(urlset)).ToList();
                ((List<Url>)site.Urls).AddRange(urlsToMeasure);

                m = await GetMeasurement(urlsToMeasure);
                site.Measurements.Add(m);

                _siterepo.InsertSite(site);

            }
            else
            {
                var sitemapXml = await Helpers.HttpRequestHelper.GetResponseString(site.SitemapAddress);
                var urlset = Helpers.XmlHelper.Deserialize(sitemapXml);

                var urlsToMeasure = GetUrls(GetLocsFromUrlset(urlset)).ToList();
                ((List<Url>)site.Urls).AddRange(urlsToMeasure);
                site.Urls = site.Urls
                    .GroupBy(u => u.Location)
                    .Select(u => u.First())
                    .ToList();
                m = await GetMeasurement(urlsToMeasure);
                site.Measurements.Add(m);

                _siterepo.UpdateSite(site);

            }

            _siterepo.Save();

            return m.MeasuredUrls;     
        }

        private static async Task<Measurement> GetMeasurement(IEnumerable<Url> urls)
        {

            var m = new Measurement();

            //todo: make not static
            var measuredUrls = await Helpers.RequestFactory.ConcurrentMeasure(urls);
            m.MeasuredUrls = measuredUrls.ToList();
            m.DateOfMeasuring = DateTime.Now;

            return m;
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