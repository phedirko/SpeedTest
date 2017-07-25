using SpeedTest.Data;
using SpeedTest.Data.Models;
using SpeedTest.Helpers;
using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SpeedTest.Services
{
    public class HomeService : IHomeService
    {
        public HomeService(ISiteRepository siteRepo, IRequestFactory reqFactory)
        {
            _siterepo = siteRepo;
            _reqFactory = reqFactory;
        }

        private readonly ISiteRepository _siterepo;
        private readonly IRequestFactory _reqFactory;

        public async Task<IEnumerable<MeasuredUrl>> ProcessRequest(string siteUrl)
        {
            string siteAddr = siteUrl;

            
            if (!siteUrl.StartsWith("http://") && !siteUrl.StartsWith("https://"))
                siteUrl = "http://" + siteUrl;


            Site site = _siterepo.GetSiteByUrl(siteUrl);
            Measurement m;
            if(site == null)
            {
                site = new Site
                {
                    Address = siteUrl,
                   
                };

                var sitemapXml = await HttpRequestHelper.GetResponseString(site.SitemapAddress);
                var urlset = XmlHelper.Deserialize(sitemapXml);

                var urlsToMeasure = GetUrls(GetLocsFromUrlset(urlset)).ToList();
                ((List<Url>)site.Urls).AddRange(urlsToMeasure);

                m = await GetMeasurement(urlsToMeasure);
                site.Measurements.Add(m);

                _siterepo.InsertSite(site);

            }
            else
            {
                var sitemapXml = await HttpRequestHelper.GetResponseString(site.SitemapAddress);
                var urlset = XmlHelper.Deserialize(sitemapXml);

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

        private async Task<Measurement> GetMeasurement(IEnumerable<Url> urls)
        {

            var m = new Measurement();

            var measuredUrls = await _reqFactory.ConcurrentMeasure(urls);
            m.MeasuredUrls = measuredUrls.ToList();
            m.DateOfMeasuring = DateTime.Now;

            return m;
        }

        private IEnumerable<string> GetLocsFromUrlset(Urlset urlset)
        {
            return urlset.Urls.Select(x => x.Loc);
        }

        private IEnumerable<Url> GetUrls(IEnumerable<string> urls)
        {
            foreach(var url in urls)
            {
                yield return new Url { Location = url };
            }
        }
    }
}