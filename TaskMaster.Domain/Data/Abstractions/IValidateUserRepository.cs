using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Entities.DTOs.Common;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IValidateUserRepository
    {
        Task<UserLogged> ValidateExternalUserAsync(string userId);
    }
}