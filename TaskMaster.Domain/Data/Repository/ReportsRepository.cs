using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Entities.DTOs.Reports;
using TaskMaster.Entities.Master;
using TaskMaster.Entities.Security;

namespace TaskMaster.Domain.Data.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IEfDbContextBase Context;

        public ReportsRepository(IEfDbContextBase context)
        {
            Context = context;
        }

        public async Task<IEnumerable<StatusTaskbyProjectOutputDTO>> GetAllStatusTaskbyProject(int lastDays = 15)
        {
            var date = DateTimeOffset.Now.AddDays(lastDays * -1);

            var result = await (from p in Context.GetDbSet<Project>()
                                join t in Context.GetDbSet<Entities.Master.Task>() on p.Id equals t.ProjectId
                                join u in Context.GetDbSet<User>() on p.CreateByUser equals u.Id
                                where p.CreationDate >= date
                                group t by new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Description,
                                    t.Status,
                                    u.FirstName,
                                    u.LastName,
                                } into g
                                select new StatusTaskbyProjectOutputDTO()
                                {
                                    Id = g.Key.Id,
                                    Name = g.Key.Name,
                                    Description = g.Key.Description,
                                    FullNameUser = $"{g.Key.FirstName} {g.Key.LastName}",
                                    Quantity = g.Count(),
                                    Status = g.Key.Status
                                }).OrderBy(x => x.Id).ToListAsync();

            return result;
        }
    }
}