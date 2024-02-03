﻿using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
