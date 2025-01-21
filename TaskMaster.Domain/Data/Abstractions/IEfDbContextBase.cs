using Microsoft.EntityFrameworkCore;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IEfDbContextBase : IUnitOfWork
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}