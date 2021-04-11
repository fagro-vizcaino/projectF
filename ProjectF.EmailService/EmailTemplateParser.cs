using ProjectF.EmailService.Templates;
using System;
using System.IO;

namespace ProjectF.EmailService
{
    public static class EmailTemplateParser
    {
        static string FindHtmlTemplate(string htmlPath)
        {
            var _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(_basePath, htmlPath);

            return path;
        }

        public static string GetTemplatePath(EmailTemplateType emailTemplate, AuthHtmlTemplateConfig template)
            => emailTemplate switch
            {
                EmailTemplateType.ForgotPassword => FindHtmlTemplate(template.ForgotPassword),
                EmailTemplateType.Register => FindHtmlTemplate(template.RegisterAccount),
                EmailTemplateType.Default => "defaultTemplate",
                _ => "no template",
            };
    }
}
