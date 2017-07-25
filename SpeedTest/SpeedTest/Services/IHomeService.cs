using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<MeasuredUrl>> ProcessRequest(string siteUrl);
    }
}
