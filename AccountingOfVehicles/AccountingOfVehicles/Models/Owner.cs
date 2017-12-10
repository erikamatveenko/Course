using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfVehicles.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Cars = new HashSet<Car>();
        }

        [Key]
        [Display(Name = "Код владельца")]
        public int OwnerID { get; set; }
        [Display(Name = "ФИО")]
        public string OwnerName { get; set; }
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? OwnerBirthDate { get; set; }
        [Display(Name = "Адрес")]
        public string OwnerAddress { get; set; }
        [Display(Name = "Номер паспорта")]
        public string OwnerPassport { get; set; }
        [Display(Name = "Номер водительского удостоверения")]
        public int? OwnerNumberOfDriverLicense { get; set; }
        [Display(Name = "Дата выдачи водительского удостоверения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? OwnerLicenseDeliveryDate { get; set; }
        [Display(Name = "Срок действия водительского удостоверения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? OwnerLicenseValidityDate { get; set; }
        [Display(Name = "Категория")]
        public string OwnerCategory { get; set; }
        [Display(Name = "Дополнительная информация")]
        public string OwnerMoreInformation { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
