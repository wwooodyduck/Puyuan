using MimeKit;

namespace PuyuanDotNet8.Dtos
{
    public class MessageDto
    {
        public MailboxAddress To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public MessageDto(string to, string subject, string content)
        {
            To = new MailboxAddress(to, to);
            Subject = subject;
            Content = content;
        }
    }
}
