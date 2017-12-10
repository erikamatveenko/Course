using AccountingOfVehicles.Models;
using AccountingOfVehicles.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class CarViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
        
        public IEnumerable<BrandNameFilter> BrandNames { get; set; }

        public BrandNameFilter CurrentBrandName { get; set; }
    }
}