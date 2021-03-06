﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }
        
        [Required(ErrorMessage = "Введите email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
