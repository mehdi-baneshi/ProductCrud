using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetItems(List<Expression<Func<T, bool>>> filters=null);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
