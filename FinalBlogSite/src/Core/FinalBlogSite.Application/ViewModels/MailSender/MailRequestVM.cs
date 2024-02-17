using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.MailSender
{
    public class MailRequestVM
    {
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
