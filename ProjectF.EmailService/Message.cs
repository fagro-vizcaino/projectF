using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ProjectF.EmailService
{
    public class Message
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsRazorTemplate { get; set; } = true;
        public dynamic Model { get; set; }

        public IFormFileCollection? Attachments { get; set; }

        public Message(IEnumerable<string> to, 
            string subject, 
            string content,
            dynamic model,
            IFormFileCollection? attachments, 
            bool isRazorTemplate = true)
        {
            To                      = new();
            IsRazorTemplate         = isRazorTemplate;
            Model                   = model;
            To.AddRange(to.Select(x => x));
            Subject                 = subject;
            Content                 = content;
            Attachments             = attachments;
        }
    }
}
