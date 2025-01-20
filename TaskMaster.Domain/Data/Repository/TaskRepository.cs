using Microsoft.EntityFrameworkCore;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Data.Repository
{
    public class TaskRepository : GenericRepository<Entities.Master.Task>, ITaskRepository
    {
        public TaskRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Entities.Master.Task>> GetAll(string ProjectId)
        {
            var rows = await (from t in Context.GetDbSet<Entities.Master.Task>()
                              where t.IsDeleted == false
                              && t.IsActive == true
                              && t.CreateByUser == LoggeduserId
                              && t.ProjectId == ProjectId
                              select new Entities.Master.Task()
                              {
                                  CreateByUser = t.CreateByUser,
                                  CreationDate = t.CreationDate,
                                  Description = t.Description,
                                  Id = t.Id,
                                  IsActive = t.IsActive,
                                  IsDeleted = t.IsDeleted,
                                  ModificationDate = t.ModificationDate,
                                  ProjectId = t.ProjectId,
                                  Title = t.Title,
                                  ChangeHistories = (from h in Context.GetDbSet<ChangeHistory>() where h.TaskId == t.Id select h).ToList(),
                                  Comments = (from c in Context.GetDbSet<Comment>() where c.TaskId == t.Id select c).ToList(),
                                  Priority = t.Priority,
                                  Status = t.Status,
                                  UpdateByUser = t.UpdateByUser
                              }
                             ).ToListAsync();
            return rows;
        }

        public async Task<Entities.Master.Task> GetById(string id)
        {
            var row = await (from t in Context.GetDbSet<Entities.Master.Task>()
                             where t.IsActive == true
                             && t.IsDeleted == false
                             && t.CreateByUser == LoggeduserId
                             && t.Id.Equals(id)
                             select new Entities.Master.Task()
                             {
                                 CreateByUser = t.CreateByUser,
                                 CreationDate = t.CreationDate,
                                 Description = t.Description,
                                 Id = t.Id,
                                 IsActive = t.IsActive,
                                 IsDeleted = t.IsDeleted,
                                 ModificationDate = t.ModificationDate,
                                 ProjectId = t.ProjectId,
                                 Title = t.Title,
                                 ChangeHistories = (from h in Context.GetDbSet<ChangeHistory>() where h.TaskId == t.Id select h).ToList(),
                                 Comments = (from c in Context.GetDbSet<Comment>() where c.TaskId == t.Id select c).ToList(),
                                 Priority = t.Priority,
                                 Status = t.Status,
                                 UpdateByUser = t.UpdateByUser
                             }).FirstOrDefaultAsync();
            return row;
        }

        public async Task<Entities.Master.Task> GetByTitle(string title)
        {
            var row = await (from t in Context.GetDbSet<Entities.Master.Task>()
                             where t.IsActive == true
                             && t.IsDeleted == false
                             && t.CreateByUser == LoggeduserId
                             && t.Title.Equals(title)
                             select new Entities.Master.Task()
                             {
                                 CreateByUser = t.CreateByUser,
                                 CreationDate = t.CreationDate,
                                 Description = t.Description,
                                 Id = t.Id,
                                 IsActive = t.IsActive,
                                 IsDeleted = t.IsDeleted,
                                 ModificationDate = t.ModificationDate,
                                 ProjectId = t.ProjectId,
                                 Title = t.Title,
                                 ChangeHistories = (from h in Context.GetDbSet<ChangeHistory>() where h.TaskId == t.Id select h).ToList(),
                                 Comments = (from c in Context.GetDbSet<Comment>() where c.TaskId == t.Id select c).ToList(),
                                 Priority = t.Priority,
                                 Status = t.Status,
                                 UpdateByUser = t.UpdateByUser
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}