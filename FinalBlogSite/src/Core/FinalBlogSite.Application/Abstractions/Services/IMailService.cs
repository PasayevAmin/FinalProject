using FinalBlogSite.Application.ViewModels.MailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestVM mailRequest);

    }
}
