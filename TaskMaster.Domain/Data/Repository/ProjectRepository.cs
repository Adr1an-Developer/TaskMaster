﻿using Microsoft.EntityFrameworkCore;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Data.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IEfDbContextBase context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var rows = await (from p in Context.GetDbSet<Project>()
                              where p.IsDeleted == false
                              && p.IsActive == true
                              && p.CreateByUser == LoggeduserId
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
                             && p.CreateByUser == LoggeduserId
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
                             && p.CreateByUser == LoggeduserId
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