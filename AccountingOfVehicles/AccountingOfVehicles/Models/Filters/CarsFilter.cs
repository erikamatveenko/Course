﻿using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.Filters
{
    public class CarsFilter : IParameters
    {
        public string brandName { get; set; }
        public string ownerName { get; set; }

        public IEnumerable<String> BrandNames { get; set; }
        public IEnumerable<String> OwnerNames { get; set; }

        public object GetParameters(int pageNumber)
        {
            return new { brandName, ownerName, pageNumber };
        }
    }
}
