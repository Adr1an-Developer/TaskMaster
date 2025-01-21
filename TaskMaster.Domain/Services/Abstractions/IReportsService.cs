using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.DTOs.Reports;

namespace TaskMaster.Domain.Services.Abstractions
{
    public interface IReportsService
    {
        Task<DataResults<StatusTaskbyProjectOutputDTO>> GetAllStatusTaskbyProject(int lastDays = 15);
    }
}