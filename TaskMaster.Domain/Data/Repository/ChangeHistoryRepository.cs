using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Data.Repository
{
    public class ChangeHistoryRepository : GenericRepository<Entities.Master.ChangeHistory>, IChangeHistoryRepository
    {
        public ChangeHistoryRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Entities.Master.ChangeHistory>> GetAll(string taskId)
        {
            var rows = await (from t in Context.GetDbSet<Entities.Master.ChangeHistory>()
                              where t.IsDeleted == false
                              && t.IsActive == true
                              && t.CreateByUser == LoggedUserId
                              && t.TaskId == t.TaskId
                              select new Entities.Master.ChangeHistory()
                              {
                                  CreateByUser = t.CreateByUser,
                                  CreationDate = t.CreationDate,
                                  Id = t.Id,
                                  IsActive = t.IsActive,
                                  IsDeleted = t.IsDeleted,
                                  ModificationDate = t.ModificationDate,
                                  UpdateByUser = t.UpdateByUser,
                                  TaskId = t.TaskId,
                                  ChangeDetails = t.ChangeDetails,
                              }
                             ).ToListAsync();
            return rows;
        }

        public async Task<Entities.Master.ChangeHistory> GetById(string id)
        {
            var row = await (from t in Context.GetDbSet<Entities.Master.ChangeHistory>()
                             where t.IsActive == true
                             && t.IsDeleted == false
                             && t.CreateByUser == LoggedUserId
                             && t.Id.Equals(id)
                             select new Entities.Master.ChangeHistory()
                             {
                                 CreateByUser = t.CreateByUser,
                                 CreationDate = t.CreationDate,
                                 Id = t.Id,
                                 IsActive = t.IsActive,
                                 IsDeleted = t.IsDeleted,
                                 ModificationDate = t.ModificationDate,
                                 UpdateByUser = t.UpdateByUser,
                                 TaskId = t.TaskId,
                                 ChangeDetails = t.ChangeDetails,
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}