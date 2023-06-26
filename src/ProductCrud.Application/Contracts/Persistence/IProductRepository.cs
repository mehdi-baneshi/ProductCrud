using ProductCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Contracts.Persistence
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> IsProductUnique(int id, string manufactureEmail, DateTime produceDate);
    }
}
