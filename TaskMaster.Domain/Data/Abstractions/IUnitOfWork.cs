using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domain.Data.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
    }
}