using TaskMaster.Entities.DTOs.Common;

namespace TaskMaster.Domain.Services.Abstractions
{
    public interface IValidateUserService
    {
        Task<DataResult<UserLogged>> ValidateUserId(string id);
    }
}