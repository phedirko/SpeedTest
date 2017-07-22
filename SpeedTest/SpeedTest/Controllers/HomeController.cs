using SpeedTest.Data;
using SpeedTest.Data.Models;
using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SpeedTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteRepository _siteRepository;

        public HomeController()
        {
            this._siteRepository = new SiteRepository(new STdbcontext());
        }

        public HomeController(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Measure(string siteUrl)
        {
            if (!siteUrl.StartsWith("http://"))
                siteUrl = "http://" + siteUrl;

            Site s = new Site
            {
                Address = siteUrl
            };

            var sitemapXml = await Helpers.HttpRequestHelper.GetResponseString(s.SitemapAddress);

            var urlsFromSitemap = Helpers.XmlHelper.Deserialize(sitemapXml);

            s.Urls = s.Urls.Take(10).ToList();

            foreach(var url in urlsFromSitemap.Urls)
            {
                s.Urls.Add(new Url { Location = url.Loc });
            }

            var m = new Measurement();
            
            foreach(var url in s.Urls)
            {

                var mUrl = new MeasuredUrl();
                mUrl.Url = url;
                
                var respTime = await Helpers.HttpRequestHelper.MeasureResponseTime(url.Location);

                mUrl.ElapsedTime = respTime;
                m.MeasuredUrls.Add(mUrl);
            }

            s.Measurements.Add(m);

            return Json(GetListOfUrlVMs(m.MeasuredUrls.ToList()));
        }

        private IEnumerable<MeasuredUrlViewModel> GetListOfUrlVMs(List<MeasuredUrl> urls)
        {
            foreach(var u in urls)
            {
                yield return new MeasuredUrlViewModel
                {
                    url = u.Url.Location,
                    time = u.ElapsedTime.TotalMilliseconds.ToString()
                };
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}