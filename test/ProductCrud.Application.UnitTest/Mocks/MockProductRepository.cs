using Castle.Core.Resource;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Domain;
using ProductCrud.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ProductCrud.Application.UnitTests.Mocks
{
    public static class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductRepository()
        {
            var products = new List<Product>
            {
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
                    CreatedBy = "test",
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
            };

            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetItems(It.IsAny<List<Expression<Func<Product, bool>>>>())).ReturnsAsync((List<Expression<Func<Product, bool>>> filters) =>
            {
                IQueryable<Product> query = products.AsQueryable();

                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        query = query.Where(filter);
                    }
                }

                return query.ToList();
            });

            mockRepo.Setup(c => c.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return products.Find(c => c.Id == id);
            });

            mockRepo.Setup(r => r.Add(It.IsAny<Product>())).ReturnsAsync((Product product) => 
            {
                product.CreatedBy= "test";
                products.Add(product);
                return product;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<Product>())).Returns((Product product) =>
            {
                foreach (var c in products.Where(c => c.Id == product.Id))
                {
                    c.IsAvailable = product.IsAvailable;
                    c.Name = product.Name;
                    c.ProduceDate = product.ProduceDate;
                    c.ManufacturePhone = product.ManufacturePhone;
                    c.ManufactureEmail = product.ManufactureEmail;
                }

                return Task.FromResult(true);
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<Product>())).Returns((Product product) =>
            {
                return Task.FromResult(products.Remove(product));
            });

            mockRepo.Setup(r => r.IsProductUnique(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync((int id,string manufactureEmail, DateTime produceDate) =>
            {
                bool isUnique = !(products.Any(c => c.Id != id && c.ManufactureEmail.ToLower() == manufactureEmail.ToLower() && c.ProduceDate == produceDate));
                return isUnique;
            });

            return mockRepo;
        }
    }
}
