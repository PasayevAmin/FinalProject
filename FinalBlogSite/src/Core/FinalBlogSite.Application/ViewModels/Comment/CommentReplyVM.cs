using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Comment
{
    public class CommentReplyVM
    {
        [Required]
        [MinLength(1)]
        [MaxLength(1024)]
        public string Text { get; set; }
        public int CommentId { get; set; }
    }
}
