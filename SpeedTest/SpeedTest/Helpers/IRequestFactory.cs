using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Helpers
{
    public interface IRequestFactory
    {
        Task<IEnumerable<MeasuredUrl>> ConcurrentMeasure(IEnumerable<Url> urls);
    }
}
