using KafeinShop.Core.DTOs;
using KafeinShop.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafeinShop.Repository.Configurations
{
    public class UserConfiguration : IdentityUser, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(15);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(15);
        }
    }
}
