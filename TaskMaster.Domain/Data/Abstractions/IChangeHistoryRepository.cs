namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IChangeHistoryRepository : IGenericRepository<Entities.Master.ChangeHistory>
    {
        Task<IEnumerable<Entities.Master.ChangeHistory>> GetAll(string TaskId);

        Task<Entities.Master.ChangeHistory> GetById(string id);
    }
}