using AccountingOfVehicles.Models;
using AccountingOfVehicles.Models.Filters;
using AccountingOfVehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class OwnerViewModel
    {
        public IEnumerable<Owner> Owners { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }

        public OwnersFilter OwnersFilters { get; set; }
    }
}
