using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class TitleAllowanceFilter : IParameters
    {
        public int Id { get; set; }
        public string TitleAllowance { get; set; }
        public object GetParameters(int pageNumber)
        {
            return new {  TitleAllowance, pageNumber };
        }
    }
}
