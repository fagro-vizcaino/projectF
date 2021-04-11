using System.Threading.Tasks;

namespace ProjectF.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message, EmailTemplateType emailTemplate);
    }
}
