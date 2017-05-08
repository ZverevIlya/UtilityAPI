using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Helper.EmailHelper
{
    public class ExceptionMailMessage : MailMessage
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ExceptionMailMessage()
        {
            this.IsBodyHtml = true;
            this.BodyEncoding = Encoding.UTF8;
            this.To.Add(new MailAddress("DEV_TEAM@163.com"));
            this.From = new MailAddress("tfsnotifier@163.com");
        }

        public void Send()
        {
            try
            {
                Email.Deliver.Send(this);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
    }
}
