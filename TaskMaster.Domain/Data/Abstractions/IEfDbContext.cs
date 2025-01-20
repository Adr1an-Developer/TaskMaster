using Microsoft.EntityFrameworkCore;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IEfDbContext : IUnitOfWork
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}