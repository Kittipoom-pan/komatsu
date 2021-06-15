using komatsu.api;
//using MimeKit;
//using System;
//using System.Collections.Generic;
//using System.Net.Mail;
//using MailKit.Net.Smtp;
//using System.Text;
//using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(Message mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailConfig.From);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest._emailConfig)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfig.UserName, _emailConfig.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        //public async Task SendEmailAsync(Message message)
        //{
        //    //var emailMessage = CreateEmailMessage(message);
        //    var emailMessage = EmailMessage(message);

        //    await SendAsync(emailMessage);
        //}
        //public async Task SendEmailAsync(Message message)
        //{
        //    var emailMessage = CreateEmailMessage(message);
        //    Send(emailMessage);
        //}

        //private MimeMessage CreateEmailMessage(Message message)
        //{
        //    var emailMessage = new MimeMessage();

        //    emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
        //    emailMessage.To.Add(MailboxAddress.Parse(message.To));
        //    // send multiple 
        //    //emailMessage.To.AddRange(message.To);
        //    emailMessage.Subject = message.Subject;
        //    //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //    {
        //        Text = string.Format("<html>" +
        //                             "<head> " +
        //                                 "<meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" > " +
        //                             "</head> " +
        //                             "<b>Template :</b> {1} <br>" +
        //                             "{0} " +
        //                             "</html> "
        //                            , message.Content, message.Template)
        //    };

        //    return emailMessage;
        //}

        //private MimeMessage CreateEmailMessage(Message message)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
        //    emailMessage.To.AddRange(message.To);
        //    emailMessage.Subject = message.Subject;
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        //    return emailMessage;
        //}
        //private void Send(MimeMessage mailMessage)
        //{
        //    using (var client = new SmtpClient())
        //    {
        //        try
        //        {
        //            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
        //            client.AuthenticationMechanisms.Remove("XOAUTH2");
        //            client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
        //            client.Send(mailMessage);
        //        }
        //        catch
        //        {
        //            //log an error message or throw an exception or both.
        //            throw;
        //        }
        //        finally
        //        {
        //            client.Disconnect(true);
        //            client.Dispose();
        //        }
        //    }
        //}

        //private MimeMessage EmailMessage(Message message)
        //{
        //    var emailMessage = new MimeMessage();

        //    emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
        //    // send multiple 
        //    emailMessage.To.AddRange(message.ToMutiple);
        //    emailMessage.Subject = message.Subject;
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //    {
        //        Text = string.Format("<html>" +
        //                             "<head> " +
        //                                 "<meta http - equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" > " +
        //                             "</head> " +
        //                             "<b>Template :</b> {1} <br>" +
        //                             "{0} " +
        //                             "</html> "
        //                            , message.Content, message.Template)
        //    };

        //    return emailMessage;
        //}

        //private async Task SendAsync(MimeMessage mailMessage)
        //{
        //    using (var client = new SmtpClient())
        //    {
        //        try
        //        {
        //            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
        //            client.AuthenticationMechanisms.Remove("XOAUTH2");
        //            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

        //            await client.SendAsync(mailMessage);
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            await client.DisconnectAsync(true);
        //            client.Dispose();
        //        }
        //    }
        //}
    }
}
