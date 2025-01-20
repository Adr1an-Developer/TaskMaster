using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    [Table("project")]
    public class Project : AuditEntity
    {
        [Column("id")]
        public string Id { get; set; } = string.Empty;

        [Column("name")]
        public string Name
        {
            get; set;
        }

        [Column("description")]
        public string Description
        {
            get; set;
        }

        [JsonIgnore]
        [NotMapped]
        public List<Task>? Tasks { get; set; } = new();

        [JsonIgnore]
        [NotMapped]
        public bool AllCompleted
        {
            get
            {
                return !Tasks.Any(x => x.Status != nameof(Enums.TaskStatus.Concluído));
            }
        }

        [JsonIgnore]
        [NotMapped]
        public bool CanCreate
        {
            get
            {
                return Tasks.Count() < 20;
            }
        }
    }
}