using KafeinShop.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafeinShop.Repository.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = " Filtre Kahve" }, 
                new Category { Id = 2, Name = " Türk Kahvesi" }, 
                new Category { Id = 3, Name = " Expresso" }, 
                new Category { Id = 4, Name = " Kafeinsiz Kahve" });
        }
    }
}
