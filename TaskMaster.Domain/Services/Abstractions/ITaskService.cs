using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;

namespace TaskMaster.Domain.Services.Abstractions
{
    public interface ITaskService : IServiceBase<Entities.Master.Task>
    {
        Task<DataResults<TaskOutputDTO>> GetAll(string projectId, UserLogged userInfo);

        Task<DataResult<TaskOutputDTO>> GetbyId(string id, UserLogged userInfo);

        Task<DataResult<TaskOutputDTO>> GetByTitle(string title, UserLogged userInfo);

        Task<DataResult<TaskOutputDTO>> Create(AddTaskDTO entity, UserLogged userInfo);

        Task<DataResult<TaskOutputDTO>> Update(Entities.Master.Task entity, UserLogged userInfo);

        Task<DataResult<TaskOutputDTO>> Delete(string id, UserLogged userInfo);
    }
}