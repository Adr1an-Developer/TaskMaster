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
        public Guid Id { get; set; } = Guid.NewGuid();

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