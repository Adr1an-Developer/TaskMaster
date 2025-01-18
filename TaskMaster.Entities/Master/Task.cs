using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Entities.Common;
using TaskMaster.Entities.Enums;

namespace TaskMaster.Entities.Master
{
    [Table("task")]
    public class Task : AuditEntity
    {
        [Column("id")]
        public Guid Id
        {
            get; set;
        }

        [Column("title")]
        public string Title
        {
            get; set;
        }

        [Column("description")]
        public string Description
        {
            get; set;
        }

        [Column("status")]
        public Enums.TaskStatus Status
        {
            get; set;
        }

        [Column("priority")]
        public TaskPriority Priority
        {
            get; set;
        }

        [NotMapped]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [NotMapped]
        public List<ChangeHistory> ChangeHistories { get; set; } = new List<ChangeHistory>();

        public List<string> CompareWith(Task oldData)
        {
            var changesList = new List<string>();
            CompareValues(oldData.Title, this.Title, "Titulo", changesList);
            CompareValues(oldData.Description, this.Description, "Descrição", changesList);
            CompareValues(nameof(oldData.Status), nameof(this.Status), "Status", changesList);
            CompareValues(nameof(oldData.Priority), nameof(this.Priority), "Prioridade", changesList);

            return changesList;
        }

        private void CompareValues(string oldValue, string newValue, string propertyName, List<string> cambios)
        {
            if (!newValue.Equals(oldValue, StringComparison.Ordinal))
            {
                cambios.Add($"{propertyName}: {oldValue} -> {newValue}");
            }
        }
    }
}