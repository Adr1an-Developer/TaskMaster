using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Security
{
    [Table("user")]
    public class User : AuditEntity
    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("profile_id")]
        public string ProfileId
        {
            get; set;
        }

        [Column("first_name")]
        public string FirstName
        {
            get; set;
        }

        [Column("last_name")]
        public string LastName
        {
            get; set;
        }

        [Column("email")]
        public string Email
        {
            get; set;
        }
    }
}