using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.Filters
{
    public class EmployeesFilter : IParameters
    {
        public string titleName { get; set; }
        public IEnumerable<string> TitleNames { get; set; }

        public object GetParameters(int pageNumber)
        {
            return new { titleName, pageNumber };
        }
    }
}
