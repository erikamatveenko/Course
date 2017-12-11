using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.Filters
{
    public class BrandsFilter : IParameters
    {
        public string brandCountry { get; set; }

        public IEnumerable<String> BrandCounties { get; set; }

        public object GetParameters(int pageNumber)
        {
            return new { brandCountry,  pageNumber };
        }
    }
}