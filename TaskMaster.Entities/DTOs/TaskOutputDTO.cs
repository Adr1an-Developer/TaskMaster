using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskMaster.Entities.Common;
using TaskMaster.Entities.Master;

namespace TaskMaster.Entities.DTOs
{
    public class TaskOutputDTO : AuditEntity
    {
        public string Id
        {
            get; set;
        }

        public string ProjectId
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }

        public string Priority
        {
            get; set;
        }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<ChangeHistory> ChangeHistories { get; set; } = new List<ChangeHistory>();
    }
}