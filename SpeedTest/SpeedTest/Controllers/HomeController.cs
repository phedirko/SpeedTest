using SpeedTest.Data;
using SpeedTest.Data.Models;
using SpeedTest.Helpers;
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
        private readonly IHomeService _homeService;

        public HomeController()
        {
            _siteRepository = new SiteRepository(new STdbcontext());
            _homeService = new HomeService(_siteRepository, new RequestFactory());
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Measure(string siteUrl)
        {
            //todo: make not static
            var measuredUrls = await _homeService.ProcessRequest(siteUrl);

            return Json(ToVM(measuredUrls).OrderByDescending(x=> x.time));
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