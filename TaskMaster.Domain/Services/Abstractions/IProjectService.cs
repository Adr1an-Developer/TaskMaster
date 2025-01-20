using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Services.Abstractions
{
    public interface IProjectService : IServiceBase<Project>
    {
        Task<DataResults<ProjectOutputDTO>> GetAll(UserLogged userInfo);

        Task<DataResult<ProjectOutputDTO>> GetbyId(string id, UserLogged userInfo);

        Task<DataResult<ProjectOutputDTO>> GetByName(string name, UserLogged userInfo);

        Task<DataResult<ProjectOutputDTO>> Create(AddProjectDTO entity, UserLogged userInfo);

        Task<DataResult<ProjectOutputDTO>> Update(Project entity, UserLogged userInfo);

        Task<DataResult<ProjectOutputDTO>> Delete(string id, UserLogged userInfo);
    }
}