using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfVehicles.Models
{
    public partial class Title
    {
        public Title()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Display(Name = "Код звания сотрудника ГАИ")]
        public int TitleID { get; set; }
        [Display(Name = "Наименование")]
        public string TitleName { get; set; }
        [Display(Name = "Надбавка")]
        public double? TitleAllowance { get; set; }
        [Display(Name = "Обязанности")]
        public string TitleCharge { get; set; }
        [Display(Name = "Требования")]
        public string TitleRequirement { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
