using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Domain.Entities;

namespace ProductCrud.Persistence.Configurations.Entities
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Breitling Watch, Top Time B01 Triumph",
                    IsAvailable = true,
                    CreatedBy="system",
                    ManufacturePhone="+989112223334",
                    ManufactureEmail = "info.us@breitling.com",
                    ProduceDate=DateTime.Now.AddYears(-1),
                },
                new Product
                {
                    Id = 2,
                    Name = "Breitling Watch, Top Time B01 Ford ThunderBird",
                    IsAvailable = false,
                    CreatedBy = "system",
                    ManufacturePhone = "+989112223334",
                    ManufactureEmail = "info.us@breitling.com",
                    ProduceDate = DateTime.Now.AddYears(-2),
                },
                new Product
                {
                    Id = 3,
                    Name = "Alpiner Watch, Heritage Carrée Mechanical 140 Years",
                    IsAvailable = true,
                    CreatedBy = "admin",
                    ManufacturePhone = "+19994445556",
                    ManufactureEmail = "customercare@us.alpinawathes.com",
                    ProduceDate = DateTime.Now.AddYears(-3),
                }
            );
        }
    }
}
