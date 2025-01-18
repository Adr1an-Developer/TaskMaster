using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork
        {
            get;
        }

        Task<IEnumerable<TEntity>> FindAllAsync();

        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> CreateAsync(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}