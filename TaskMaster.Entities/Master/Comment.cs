using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    [Table("comment")]
    public class Comment : AuditEntity
    {
        [Column("id")]
        public string Id { get; set; } = string.Empty;

        [Column("task_id")]
        public string TaskId
        {
            get; set;
        }

        [Column("description")]
        public string Description
        {
            get; set;
        }
    }
}