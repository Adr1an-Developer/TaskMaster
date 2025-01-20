namespace TaskMaster.Domain.Data.Abstractions
{
    public interface ITaskRepository : IGenericRepository<Entities.Master.Task>
    {
        Task<IEnumerable<Entities.Master.Task>> GetAll(string ProjectId);

        Task<Entities.Master.Task> GetById(string id);

        Task<Entities.Master.Task> GetByTitle(string name);
    }
}