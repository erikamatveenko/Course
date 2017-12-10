using AccountingOfVehicles.Models;
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

        public IEnumerable<TitleNameFilter> TitleNames { get; set; }

        public TitleNameFilter CurrentTitleName { get; set; }
    }
}
