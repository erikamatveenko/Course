using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AccountingOfVehicles.Services;

namespace AccountingOfVehicles.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Подтвердите ваш email",
                $"Пожалуйста, для подтверждения вашего аккаунта нажмите на эту ссылку: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
