using IdentityWithExistingDb.Core.Consts;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.EF.Managers
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();

            message.To.Add(email);
            message.From = new MailAddress(MailConsts.FromMail, MailConsts.DisplayName);
            message.Subject = subject;
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml= true;

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = MailConsts.OutlookProviderHost,
                Port = MailConsts.OutlookProviderPort,
                Credentials = new NetworkCredential(MailConsts.FromMail, MailConsts.FromPassword),
                EnableSsl= true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
