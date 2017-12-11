using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class BrandNameFilter : IParameters
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public object GetParameters(int pageNumber)
        {
            return new {  BrandName, pageNumber };
        }
    }
}
