using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class TitleNameFilter : IParameters
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        public object GetParameters(int pageNumber)
        {
            return new {  TitleName, pageNumber };
        }
    }
}
