using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.DTOs
{
    public class ChangeHistoryOutputDTO : AuditEntity
    {
        public string Id { get; set; } = string.Empty;

        public string TaskId
        {
            get; set;
        }

        public string ChangeDetails
        {
            get; set;
        }
    }
}