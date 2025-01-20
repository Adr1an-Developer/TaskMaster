using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Entities.DTOs
{
    public class AddChangeHistoryDTO
    {
        [Required]
        public string TaskId
        {
            get; set;
        }

        [Required]
        public string ChangeDetails
        {
            get; set;
        }
    }
}