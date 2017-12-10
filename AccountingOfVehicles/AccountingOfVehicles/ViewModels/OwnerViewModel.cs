using AccountingOfVehicles.Models;
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

        public IEnumerable<OwnerBirthDateFilter> OwnerBirthDates { get; set; }

        public OwnerBirthDateFilter CurrentOwnerBirthDate { get; set; }
    }
}
