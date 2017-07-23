using SpeedTest.Data;
using SpeedTest.Data.Models;
using SpeedTest.Models;
using SpeedTest.Services;
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
            //todo: make not static
            var measuredUrls = await HomeService.ProcessRequest(siteUrl);

            return Json(ToVM(measuredUrls));
        }

        private IEnumerable<MeasuredUrlViewModel> ToVM(IEnumerable<MeasuredUrl> mUrls)
        {
            foreach(var url in mUrls)
            {
                yield return new MeasuredUrlViewModel { url = url.Url, time = url.ElapsedTime.ToString() };
            }
        }


        public ActionResult About()
        {
            var x = _siteRepository.GetSites().ToList();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}