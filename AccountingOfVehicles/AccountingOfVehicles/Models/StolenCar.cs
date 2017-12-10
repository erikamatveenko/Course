using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfVehicles.Models
{
    public partial class StolenCar
    {
        [Key]
        [Display(Name = "Код автомобиля в угоне")]
        public int StolenCarID { get; set; }
        public int? CarID { get; set; }
        public int? EmployeeID { get; set; }
        [Display(Name = "Дата угона")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? StolenCarStealingDate { get; set; }
        [Display(Name = "Дата заявления об угоне")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? StolenCarStatementDate { get; set; }
        [Display(Name = "Вид страховки")]
        public string StolenCarInsuranceType { get; set; }
        [Display(Name = "Обстоятельства угона")]
        public string StolenCarCondition { get; set; }
        [Display(Name = "Отметка о нахождении")]
        public string StolenCarFind { get; set; }
        [Display(Name = "Дата нахождения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? StolenCarFindDate { get; set; }

        public virtual Car Car { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
