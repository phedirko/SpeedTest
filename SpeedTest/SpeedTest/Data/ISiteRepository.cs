using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Data
{
    public interface ISiteRepository :IDisposable
    {
        IEnumerable<Site> GetSites();
        Site GetSiteById(string id);
        void InsertSite(Site site);
        void DeleteSite(string id);
        void UpdateSite(Site site);
        void Save();
    }
}
