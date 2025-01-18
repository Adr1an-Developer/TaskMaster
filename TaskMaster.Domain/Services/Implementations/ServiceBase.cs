using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Services.Abstractions;

namespace TaskMaster.Domain.Services.Implementations
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repositoryBase;

        public ServiceBase(IGenericRepository<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase ?? throw new ArgumentNullException(nameof(repositoryBase));
        }
    }
}