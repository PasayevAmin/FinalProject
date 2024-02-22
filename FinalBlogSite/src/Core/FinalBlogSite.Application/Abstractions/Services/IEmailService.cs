using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(string emailTo, string subject, string body, bool isHTML = false);
    }
}
