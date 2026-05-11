using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITGBrands.Common.Models;

namespace ITGBrands.Common.Models
{
    public static class DataCache
    {
        public static ApiResponse CachedData { get; set; }
        public static DateTime CacheExpiration { get; set; } = DateTime.MinValue;

        public static bool IsCacheValid()
        {
            return DateTime.Now < CacheExpiration;
        }
    }

}
