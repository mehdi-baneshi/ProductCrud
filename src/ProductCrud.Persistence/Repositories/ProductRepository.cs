using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Domain.Common;
using ProductCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsProductUnique(int id, string manufactureEmail, DateTime produceDate)
        {
            bool isUnique = !(await _dbContext.Products.AnyAsync(c => c.Id != id && c.ManufactureEmail.ToLower() == manufactureEmail.ToLower() && c.ProduceDate == produceDate));

            return isUnique;
        }
    }
}
