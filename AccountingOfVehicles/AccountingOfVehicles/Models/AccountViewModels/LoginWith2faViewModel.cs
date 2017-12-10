using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "Введите код аутентификации.")]
        [StringLength(7, ErrorMessage = "{0} должен быть не менее {2} и не более {1} символов в длину.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Код аутентификации")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Запомнить это устройство")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
