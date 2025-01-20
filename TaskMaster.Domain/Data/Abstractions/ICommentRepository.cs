namespace TaskMaster.Domain.Data.Abstractions
{
    public interface ICommentRepository : IGenericRepository<Entities.Master.Comment>
    {
        Task<IEnumerable<Entities.Master.Comment>> GetAll(string TaskId);

        Task<Entities.Master.Comment> GetById(string id);
    }
}