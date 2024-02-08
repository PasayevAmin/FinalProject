using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Tokens
{
    public class TokenResponseVM
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
