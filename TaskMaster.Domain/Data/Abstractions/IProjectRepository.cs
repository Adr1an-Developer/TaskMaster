using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetAll();

        Task<Project> GetById(string id);

        Task<Project> GetByName(string name);
    }
}