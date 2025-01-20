using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Entities.DTOs
{
    public class AddTaskDTO
    {
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

        public string UserId
        {
            get; set;
        }
    }
}