using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Entities.DTOs
{
    public class AddTaskDTO
    {
        [Required]
        public string ProjectId
        {
            get; set;
        }

        [Required]
        public string Title
        {
            get; set;
        }

        [Required]
        public string Description
        {
            get; set;
        }

        [Required]
        public string Status
        {
            get; set;
        }

        [Required]
        public string Priority
        {
            get; set;
        }

        [Required]
        public string UserId
        {
            get; set;
        }
    }
}