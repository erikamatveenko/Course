using AccountingOfVehicles.Models;
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

        public IEnumerable<BrandNameFilter> BrandNames { get; set; }

        public BrandNameFilter CurrentBrandName { get; set; }
    }
}
