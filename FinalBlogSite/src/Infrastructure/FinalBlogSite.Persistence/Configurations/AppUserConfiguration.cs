using FinalBlogSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Configurations
{
    public class AppUserConfiguration:IEntityTypeConfiguration<AppUser>
    {
        

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(25);
            builder.Property(x=>x.LastName).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Email).IsRequired();
        }
    }
}
