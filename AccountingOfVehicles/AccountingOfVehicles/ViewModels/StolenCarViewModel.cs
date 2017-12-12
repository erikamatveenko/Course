using AccountingOfVehicles.Models;
using AccountingOfVehicles.Models.Filters;
using AccountingOfVehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class StolenCarViewModel
    {
        public IEnumerable<StolenCar> StolenCars { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }

        public StolenCarsFilter StolenCarsFilters { get; set; }
    }
}
