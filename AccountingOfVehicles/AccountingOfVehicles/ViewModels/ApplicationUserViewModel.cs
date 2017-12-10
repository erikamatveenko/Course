using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Уровень доступа")]
        public string RoleName { get; set; }
    }
}
