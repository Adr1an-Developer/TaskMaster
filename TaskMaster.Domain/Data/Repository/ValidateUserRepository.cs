using Microsoft.EntityFrameworkCore;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Security;

namespace TaskMaster.Domain.Data.Repository
{
    public class ValidateUserRepository : IValidateUserRepository
    {
        protected readonly IEfDbContextBase Context;

        public ValidateUserRepository(IEfDbContextBase context)
        {
            Context = context;
        }

        public async Task<UserLogged> ValidateExternalUserAsync(string userId)
        {
            var row = await (from u in Context.GetDbSet<User>()
                             join p in Context.GetDbSet<Profile>() on u.ProfileId equals p.Id
                             where u.Id == userId
                             && u.IsActive == true
                             && u.IsDeleted == false
                             select new UserLogged()
                             {
                                 userId = u.Id,
                                 IsManager = p.Name == "manager"
                             }).FirstOrDefaultAsync();

            return row;
        }
    }
}