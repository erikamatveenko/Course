using AccountingOfVehicles.Models;
using AccountingOfVehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class BrandViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<BrandCountryFilter> BrandCountries { get; set; }

        public BrandCountryFilter CurrentBrandCountry { get; set; }
    }
}
