using TaskMaster.Entities.DTOs.Reports;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IReportsRepository
    {
        Task<IEnumerable<StatusTaskbyProjectOutputDTO>> GetAllStatusTaskbyProject(int lastDays = 15);
    }
}