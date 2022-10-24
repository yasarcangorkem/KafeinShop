using KafeinShop.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafeinShop.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Fitre kahve 1",
                Price = 100,
                Stock = 20,
                CreateDate = new DateTime(2022,1,1,12,59,59)

            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Fitre kahve 2",
                Price = 200,
                Stock = 30,
                CreateDate = new DateTime(2022, 1, 1, 12, 59, 59)
            },
            new Product
            {
                Id = 3,
                CategoryId = 3,
                Name = "Expresso 1",
                Price = 600,
                Stock = 60,
                CreateDate = new DateTime(2022, 1, 1, 12, 59, 59)

            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "Türk Kahvesi 1",
                Price = 600,
                Stock = 60,
                CreateDate = new DateTime(2022, 1, 1, 12, 59, 59)

            },
            new Product
            {
                Id = 5,
                CategoryId = 2,
                Name = "Türk Kahvesi 2",
                Price = 600,
                Stock = 60,
                CreateDate = new DateTime(2022, 1, 1, 12, 59, 59)

            });
        }
    }
}
