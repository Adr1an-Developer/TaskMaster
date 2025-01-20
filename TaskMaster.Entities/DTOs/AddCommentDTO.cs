using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Entities.DTOs
{
    public class AddCommentDTO
    {
        [Required]
        public string TaskId
        {
            get; set;
        }

        [Required]
        public string Description
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