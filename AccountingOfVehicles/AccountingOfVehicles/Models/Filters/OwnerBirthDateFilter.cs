using AccountingOfVehicles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models
{
    public class OwnerBirthDateFilter : IParameters
    {
        public int Id { get; set; }
        public DateTime? OwnerBirthDate { get; set; }
        public object GetParameters(int pageNumber)
        {
            return new {  OwnerBirthDate , pageNumber};
        }
    }
}
