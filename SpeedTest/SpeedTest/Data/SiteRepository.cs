using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeedTest.Models;

namespace SpeedTest.Data.Models
{
    public class SiteRepository : ISiteRepository, IDisposable
    {
        private STdbcontext _context;

        public SiteRepository(STdbcontext context)
        {
            this._context = context;
        }

        public IEnumerable<Site> GetSites()
        {
            return _context.Sites;
        }

        public Site GetSiteById(string id)
        {
            return _context.Sites.Find(id);
        }

        public Site GetSiteByUrl(string url)
        {
            return _context.Sites.Where(x => x.Address.Equals(url)).FirstOrDefault();
        }

        public void InsertSite(Site site)
        {
            _context.Sites.Add(site);
        }

        public void DeleteSite(string id)
        {
            var site = _context.Sites.Find(id);
            _context.Sites.Remove(site);
        }

        public void UpdateSite(Site site)
        {
            _context.Entry(site).State = System.Data.Entity.EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}