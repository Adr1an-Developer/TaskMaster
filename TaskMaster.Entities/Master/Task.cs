using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    [Table("task")]
    public class Task : AuditEntity
    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("project_id")]
        public string ProjectId
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

        [Required]
        [Column("expiration_date")]
        public DateTimeOffset ExpirationDate
        {
            get; set;
        }

        [Column("status")]
        public string Status
        {
            get; set;
        }

        [Column("priority")]
        public string Priority
        {
            get; set;
        }

        [JsonIgnore]
        [NotMapped]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [JsonIgnore]
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