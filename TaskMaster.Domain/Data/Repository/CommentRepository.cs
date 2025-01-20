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
    public class CommentRepository : GenericRepository<Entities.Master.Comment>, ICommentRepository
    {
        public CommentRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Entities.Master.Comment>> GetAll(string taskId)
        {
            var rows = await (from t in Context.GetDbSet<Entities.Master.Comment>()
                              where t.IsDeleted == false
                              && t.IsActive == true
                              && t.CreateByUser == LoggedUserId
                              && t.TaskId == t.TaskId
                              select new Entities.Master.Comment()
                              {
                                  CreateByUser = t.CreateByUser,
                                  CreationDate = t.CreationDate,
                                  Id = t.Id,
                                  IsActive = t.IsActive,
                                  IsDeleted = t.IsDeleted,
                                  ModificationDate = t.ModificationDate,
                                  UpdateByUser = t.UpdateByUser,
                                  TaskId = t.TaskId,
                                  Description = t.Description,
                              }
                             ).ToListAsync();
            return rows;
        }

        public async Task<Entities.Master.Comment> GetById(string id)
        {
            var row = await (from t in Context.GetDbSet<Entities.Master.Comment>()
                             where t.IsActive == true
                             && t.IsDeleted == false
                             && t.CreateByUser == LoggedUserId
                             && t.Id.Equals(id)
                             select new Entities.Master.Comment()
                             {
                                 CreateByUser = t.CreateByUser,
                                 CreationDate = t.CreationDate,
                                 Id = t.Id,
                                 IsActive = t.IsActive,
                                 IsDeleted = t.IsDeleted,
                                 ModificationDate = t.ModificationDate,
                                 UpdateByUser = t.UpdateByUser,
                                 TaskId = t.TaskId,
                                 Description = t.Description,
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}