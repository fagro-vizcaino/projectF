using System;

namespace ProjectF.EmailService
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SendGridConfiguration
    {
        public string Sender { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
    }
}
