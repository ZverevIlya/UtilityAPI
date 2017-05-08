using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Helper.EmailHelper
{
    public class MailSender
    {
        public static void Send(string to, string subject, string body, string[] emailCCList, bool enableEmail = true)
        {
            MailMessage message = new MailMessage();

            message.To.Add(to);
            if (enableEmail)
            {
                foreach (var item in emailCCList)
                {
                    message.CC.Add(item + "@163.com");
                }

            }
            message.Bcc.Add("DEV_TEAM@163.com");

            message.From = new MailAddress("tfsnotifier@163.com");
            message.Subject = subject;
            message.IsBodyHtml = true;

            message.Body = body;

            Email.Deliver.Send(message);
        }
    }
}
