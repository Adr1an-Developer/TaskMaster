using Microsoft.EntityFrameworkCore;
using TaskMaster.Domain.Data.Abstractions;

namespace TaskMaster.Domain.Data.Contexts
{
    public class EfDbContextBase : DbContext, IEfDbContext
    {
        private DbContextOptions<EfDbContextBase> options;

        public EfDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public Task<int> SaveAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}