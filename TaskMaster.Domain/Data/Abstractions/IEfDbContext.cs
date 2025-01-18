using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IEfDbContext : IUnitOfWork
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}