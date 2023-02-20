using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.Core.Consts
{
    public class MailConsts
    {
        public readonly static string FromMail = "Mahmoud.Ibrahim97@outlook.com";
        public readonly static string DisplayName = "Identity Manager";
        public readonly static string FromPassword = "M@hmoud1997";
        public readonly static string OutlookProviderHost = "smtp-mail.outlook.com";
        public readonly static int OutlookProviderPort = 587;
    }
}
