using AccountingOfVehicles.Models;
using AccountingOfVehicles.Models.Filters;
using AccountingOfVehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }

        public TitlesFilter TitlesFilters { get; set; }
    }
}
