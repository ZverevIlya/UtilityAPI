using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Reflection;

namespace Helper.EmailHelper
{
    public class Email
    {
        /// <summary>
        /// Smtp client
        /// if using this deliver in workflow such as BuildActivity, you must also checkin the file UIH.CM.Utility.dll.config together.
        /// </summary>
        public static SmtpClient Deliver
        {
            get
            {
                string host = Settings.GetValue("SmtpHost", string.Empty);
                if (string.IsNullOrEmpty(host))
                {
                    host = "mail.163.com";
                }

                string userName =  Settings.GetValue("SmtpUsername", string.Empty);
                string cryptPasswd = Settings.GetValue("SmtpPassword", string.Empty);

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(cryptPasswd))
                {
                    // try to get username and cryptpassword from assembly.config
                    var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof(Email)).Location);
                    if (appConfig != null)
                    {
                        var usr = appConfig.AppSettings.Settings["SmtpUsername"];
                        var pwd = appConfig.AppSettings.Settings["SmtpPassword"];
                        //var hst = appConfig.AppSettings.Settings["SmtpHost"];
                        if (usr != null)
                        {
                            userName = usr.Value;
                        }
                        if (pwd != null)
                        {
                            cryptPasswd = pwd.Value;
                        }
                    }

                    // check again
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(cryptPasswd))
                    {
                        throw new ConfigurationErrorsException("Not found SmtpUsername or SmtpPassword from App.config");
                    }
                }

                string password = Security.Decryption(cryptPasswd);

                SmtpClient client = new SmtpClient(host);
                client.Credentials = new NetworkCredential(userName, password);
                //client.UseDefaultCredentials = false;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.EnableSsl = true;
                return client;
            }
        }
    }
}
