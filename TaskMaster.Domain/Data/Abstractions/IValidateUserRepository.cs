using TaskMaster.Entities.DTOs.Common;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IValidateUserRepository
    {
        Task<UserLogged> ValidateExternalUserAsync(string userId);
    }
}