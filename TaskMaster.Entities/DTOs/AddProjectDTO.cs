using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Entities.DTOs
{
    public class AddProjectDTO
    {
        [Required]
        public string Name
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