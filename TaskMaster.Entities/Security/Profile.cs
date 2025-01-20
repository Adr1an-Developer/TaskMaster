using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Security
{
    [Table("profiles")]
    public class Profile : AuditEntityNotUserFields
    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("name")]
        public string Name
        {
            get; set;
        }
    }
}