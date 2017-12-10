using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfVehicles.Models
{
    public partial class Employee
    {
        public Employee()
        {
            StolenCars = new HashSet<StolenCar>();
        }

        [Key]
        [Display(Name = "Код сотрудника ГАИ")]
        public int EmployeeID { get; set; }
        public int? TitleID { get; set; }
        [Display(Name = "ФИО")]
        public string EmployeeName { get; set; }
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? EmployeeBirthDate { get; set; }

        public virtual Title Title { get; set; }
        public ICollection<StolenCar> StolenCars { get; set; }
    }
}