using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required(ErrorMessage = "Введите код восстановления.")]
        [DataType(DataType.Text)]
        [Display(Name = "Код восстановления")]
        public string RecoveryCode { get; set; }
    }
}