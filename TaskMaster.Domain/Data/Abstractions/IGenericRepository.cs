using System.Linq.Expressions;
using TaskMaster.Entities.DTOs.Common;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork
        {
            get;
        }

        string LoggedUserId
        {
            get;
        }

        bool IsManager
        {
            get;
        }

        void SetLoggedUserInfo(UserLogged userInfo);

        Task<IEnumerable<TEntity>> FindAllAsync();

        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> CreateAsync(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}