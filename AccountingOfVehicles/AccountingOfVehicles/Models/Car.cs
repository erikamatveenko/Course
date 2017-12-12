using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfVehicles.Models
{
    public partial class Car
    {
        public Car()
        {
            StolenCars = new HashSet<StolenCar>();
        }

        [Key]
        [Display(Name = "Код автомобиля")]
        public int CarID { get; set; }
        public int? BrandID { get; set; }
        public int? OwnerID { get; set; }
        [Display(Name = "Регистрационный номер")]
        public string CarRegistrationNumber { get; set; }
        [Display(Name = "Фото")]
        public string CarPhoto { get; set; }
        [Display(Name = "Номер кузова")]
        public string CarNumberOfBody { get; set; }
        [Display(Name = "Номер двигателя")]
        public string CarNumberOfMotor { get; set; }
        [Display(Name = "Номер техпаспорта")]
        public string CarNumberOfPassport { get; set; }
        [Display(Name = "Дата выпуска")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CarReleaseDate { get; set; }
        [Display(Name = "Дата регистрации")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CarRegistrationDate { get; set; }
        [Display(Name = "Дата последнего технического осмотра")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CarLastCheckupDate { get; set; }
        [Display(Name = "Цвет")]
        public string CarColor { get; set; }
        [Display(Name = "Описание")]
        public string CarDescription { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Owner Owner { get; set; }
        public ICollection<StolenCar> StolenCars { get; set; }

    }
}
