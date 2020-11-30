using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF.EmailService
{
    public static class EmailTemplateParser
    {
        public static string FindHtmlTemplate(string htmlPath)
        {
            var _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(_basePath, htmlPath);

            return File.ReadAllText(path);
        }
    }
}
