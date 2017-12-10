using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfVehicles.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Cars = new HashSet<Car>();
        }

        [Key]
        [Display(Name = "Код марки автомобиля")]
        public int BrandID { get; set; }
        [Display(Name = "Наименование")]
        public string BrandName { get; set; }
        [Display(Name = "Фирма-производитель")]
        public string BrandCompany { get; set; }
        [Display(Name = "Страна-производитель")]
        public string BrandCountry { get; set; }
        [Display(Name = "Дата начала производства")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? BrandStartDate { get; set; }
        [Display(Name = "Дата окончания производства")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? BrandEndingDate { get; set; }
        [Display(Name = "Характеристики")]
        public string BrandCharacteristic { get; set; }
        [Display(Name = "Категория")]
        public string BrandCategory { get; set; }
        [Display(Name = "Описание")]
        public string BrandDescription { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
