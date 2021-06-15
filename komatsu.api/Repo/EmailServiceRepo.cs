using komatsu.api.Interface;
using komatsu.api.Model;
using komatsu.api.Model.Request;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Net.Mail;
using System.Text;

namespace komatsu.api.Repo
{
    public class EmailServiceRepo : IEmailServiceRepo
    {
        EmailConfig _emailConfig = null;
        public EmailServiceRepo(IOptions<EmailConfig> options)
        {
            _emailConfig = options.Value;
        }
        public bool SendEmail(EmailData emailData)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.google.com");
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("email-smtp.ap-southeast-1.amazonaws.com");

                // SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                mail.From = new MailAddress("account-for-testing-only@dev.fysvc.com");
                mail.To.Add("kittipoom@feyverly.com");
                mail.Subject = "kittipoom@feyverly.com";
                mail.Body = "kittipoom@feyverly.com";
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                //SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("AKIA3PLF7BQXMTLCB6UL", "BNi+pVyHhMghNZDHTlVgYXgs7+AAy8C1rmhFlUZ7CZzH");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                mail.Dispose();

                //MailMessage mail = new MailMessage();
                //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                //mail.From = new MailAddress("kittipoom@feyverly.com", "Official Test E-mail");
                //mail.To.Add(new MailAddress("kittipoom@feyverly.com"));
                //mail.Subject = "This is office test send email from server with ref key: ";
                //mail.Body = string.Format(
                //    "<html>" +
                //         "<u> Official Test Mail. </u><br> " +
                //         "<br> " +
                //         "<br> " +
                //         "<br> " +
                //         "Mail send by customer enviroment <br> " +
                //      "</html> ");

                //mail.IsBodyHtml = true;
                //mail.BodyEncoding = Encoding.UTF8;
                //smtp.Host = "smtp.sendgrid.net";
                //smtp.Port = 587;
                //smtp.Credentials = new System.Net.NetworkCredential("S58C+v5AWIhm4TtqcVbHqQ==", "1VPNpm9xN0kamBpiJ+6sX4YxHu4BsFSRLM6GpVBQvhCHS1c6y/ofbLYZ+8rzFjQlBN7OiSNfLDH9BSlOG9mTjJVqS+y3T3zdUuC1QQf/Ui1+WfyCT7vGs2TvJPGsg0hs2kfYx3nWFvJMhcOEqLTq3ueyGz/TrLKkLQLdmS7F07LorYD1OgRisugivT32LObB");
                //smtp.EnableSsl = false;

                //smtp.Send(mail);
                //var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse("account-for-testing-only@dev.fysvc.com"));
                //email.To.Add(MailboxAddress.Parse("kittipoom@feyverly.com"));
                //email.Subject = "Test Email Subject";
                //email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Example HTML Message Body</h1>" };

                //// send email
                //using var smtp = new SmtpClient();
                //smtp.Connect("smtp.sendgrid.net", 587, false);
                //smtp.Authenticate("kittipoom@feyverly.com", "New123948");
                //smtp.Send(email);
                //smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}
