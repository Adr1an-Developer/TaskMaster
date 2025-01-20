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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var rows = await (from p in Context.GetDbSet<Project>()
                              where p.IsDeleted == false
                              && p.IsActive == true
                              && p.CreateByUser == LoggedUserId
                              select new Project()
                              {
                                  CreateByUser = p.CreateByUser,
                                  CreationDate = p.CreationDate,
                                  Description = p.Description,
                                  Id = p.Id,
                                  IsActive = p.IsActive,
                                  IsDeleted = p.IsDeleted,
                                  ModificationDate = p.ModificationDate,
                                  Name = p.Name,
                                  Tasks = (from t in Context.GetDbSet<Entities.Master.Task>() where t.ProjectId == p.Id select t).ToList(),
                                  UpdateByUser = p.UpdateByUser
                              }
                             ).ToListAsync();
            return rows;
        }

        public async Task<Project> GetById(string id)
        {
            var row = await (from p in Context.GetDbSet<Project>()
                             where p.IsActive == true
                             && p.IsDeleted == false
                             && p.CreateByUser == LoggedUserId
                             && p.Id.Equals(id)
                             select new Project()
                             {
                                 CreateByUser = p.CreateByUser,
                                 CreationDate = p.CreationDate,
                                 Description = p.Description,
                                 Id = p.Id,
                                 IsActive = p.IsActive,
                                 IsDeleted = p.IsDeleted,
                                 ModificationDate = p.ModificationDate,
                                 Name = p.Name,
                                 Tasks = (from t in Context.GetDbSet<Entities.Master.Task>() where t.ProjectId == p.Id select t).ToList(),
                                 UpdateByUser = p.UpdateByUser
                             }).FirstOrDefaultAsync();
            return row;
        }

        public async Task<Project> GetByName(string name)
        {
            var row = await (from p in Context.GetDbSet<Project>()
                             where p.IsActive == true
                             && p.IsDeleted == false
                             && p.CreateByUser == LoggedUserId
                             && p.Name.Equals(name)
                             select new Project()
                             {
                                 CreateByUser = p.CreateByUser,
                                 CreationDate = p.CreationDate,
                                 Description = p.Description,
                                 Id = p.Id,
                                 IsActive = p.IsActive,
                                 IsDeleted = p.IsDeleted,
                                 ModificationDate = p.ModificationDate,
                                 Name = p.Name,
                                 Tasks = (from t in Context.GetDbSet<Entities.Master.Task>() where t.ProjectId == p.Id select t).ToList(),
                                 UpdateByUser = p.UpdateByUser
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}