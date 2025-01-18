using Microsoft.EntityFrameworkCore;
using TaskMaster.Entities.Master;
using TaskMaster.Entities.Security;

namespace TaskMaster.Domain.Data.Contexts
{
    public class DatabaseContext : EfDbContextBase
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User>? users
        {
            get; set;
        }

        public DbSet<Profile>? profiles
        {
            get; set;
        }

        public DbSet<Project>? projects
        {
            get; set;
        }

        public DbSet<Entities.Master.Task>? tasks
        {
            get; set;
        }

        public DbSet<Comment>? comments
        {
            get; set;
        }

        public DbSet<ChangeHistory>? changeHistory
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}