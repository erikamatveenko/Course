﻿using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.Filters
{
    public class CarsFilter : IParameters
    {
        public string brandName { get; set; }
        public string ownerName { get; set; }
        public string carNumberOfMotor { get; set; }
        public string startRegistrationDate { get; set; }
        public string endRegistrationDate { get; set; }
        
        public IEnumerable<String> BrandNames { get; set; }
        public IEnumerable<String> OwnerNames { get; set; }
        public IEnumerable<String> CarNumbersOfMotor { get; set; }


        public object GetParameters(int pageNumber)
        {
            return new { brandName, ownerName,carNumberOfMotor, startRegistrationDate, endRegistrationDate, pageNumber };
        }
    }
}
