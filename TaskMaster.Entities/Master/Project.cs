using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    [Table("project")]
    public class Project : AuditEntity
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        public string Name
        {
            get; set;
        }

        [NotMapped]
        public List<Task> Tasks { get; set; } = new();
    }
}