using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    [Table("history")]
    public class ChangeHistory : AuditEntity

    {
        [Column("id")]
        public string Id { get; set; } = string.Empty;

        [Column("task_id")]
        public string TaskId
        {
            get; set;
        }

        [Column("change_details")]
        public string ChangeDetails
        {
            get; set;
        }
    }
}