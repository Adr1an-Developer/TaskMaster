using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Services.Abstractions
{
    public interface IValidateUserService
    {
        Task<DataResult<UserLogged>> ValidateUserId(string id);
    }
}