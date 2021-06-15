using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace EmailService
{
    public class Message
    {
        //public List<MailboxAddress> ToMutiple { get; set; }
        ////public string To { get; set; }
        //public string Subject { get; set; }
        //public string Content { get; set; }
        //public string Template { get; set; }
        //public Message(IEnumerable<string> to, string subject, string content)
        //{
        //    ToMutiple = new List<MailboxAddress>();

        //    ToMutiple.AddRange(to.Select(x => new MailboxAddress(x)));
        //    Subject = subject;
        //    Content = content;
        //}
        //public List<MailboxAddress> To { get; set; }
        //public string Subject { get; set; }
        //public string Content { get; set; }
        //public Message(IEnumerable<string> to, string subject, string content)
        //{
        //    To = new List<MailboxAddress>();
        //    To.AddRange(to.Select(x => new MailboxAddress(x)));
        //    Subject = subject;
        //    Content = content;
        //}

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}