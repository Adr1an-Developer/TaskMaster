using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.DTOs
{
    public class ProjectOutputDTO : AuditEntity
    {
        public string Id { get; set; } = string.Empty;

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public List<Entities.Master.Task>? Tasks { get; set; } = new();

        public bool AllCompleted
        {
            get; set;
        }

        public bool CanCreate
        {
            get; set;
        }
    }
}