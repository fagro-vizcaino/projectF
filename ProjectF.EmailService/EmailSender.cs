using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.EmailService.Templates;
using static ProjectF.EmailService.EmailTemplateParser;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using System;

namespace ProjectF.EmailService
{
    //Extracted from https://code-maze.com/email-confirmation-aspnet-core-identity/

    public class EmailSender : IEmailSender
    {
        private readonly SendGridConfiguration _emailConfig;
        readonly AuthHtmlTemplateConfig _authTemplatePath;
        readonly IFluentEmailFactory _emailClientFactory;
        public EmailSender(SendGridConfiguration emailConfig
            , AuthHtmlTemplateConfig authHtml
            , IFluentEmailFactory emailClientFactory)
        {
            _emailConfig                 = emailConfig;
            _authTemplatePath            = authHtml;
            _emailClientFactory          = emailClientFactory;
        }

        public void SendEmail(Message message)
        {
            try
            {
                var emailMessage = CreateEmailMessage(message, EmailTemplateType.Default);
                Send(emailMessage);
            }
            catch (Exception ex) { throw; }
            
        }

        public async Task SendEmailAsync(Message message, EmailTemplateType emailTemplate)
        {
            try
            {
                var mailMessage = CreateEmailMessage(message, emailTemplate);
                await SendAsync(mailMessage);
            }
            catch (Exception ex){ throw; }
            
        }

        private IFluentEmail CreateEmailMessage(Message message, EmailTemplateType emailTemplateType)
        {

            var attachements = new List<Attachment>();
            if (message.Attachments is not null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using var ms = new MemoryStream();
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                    attachements.Add(new Attachment { Filename = attachment.FileName, Data = ms, ContentType = attachment.ContentType });
                }
            }
            var createdEmail = _emailClientFactory
                  .Create()
                  .To(string.Join(";",message.To))
                  .Subject(message.Subject)
                  .Attach(attachements);

            
            if (!message.IsRazorTemplate) return createdEmail.Body(message.Content);

            var path = GetTemplatePath(emailTemplateType, _authTemplatePath);

            return createdEmail.UsingTemplateFromFile(path, message.Model);
        }

        private void Send(IFluentEmail fluentEmail)
            => fluentEmail.Send();

        private async Task SendAsync(IFluentEmail fluentEmail)
            => await fluentEmail.SendAsync();
    }
}
