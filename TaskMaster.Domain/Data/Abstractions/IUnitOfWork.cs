namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
    }
}