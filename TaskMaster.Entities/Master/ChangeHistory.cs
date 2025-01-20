using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    [Table("history")]
    public class ChangeHistory : AuditEntity

    {
        [Column("id")]
        public string Id { get; set; } = string.Empty;

        public Guid TaskId
        {
            get; set;
        }

        public string ChangeDetails
        {
            get; set;
        }
    }
}