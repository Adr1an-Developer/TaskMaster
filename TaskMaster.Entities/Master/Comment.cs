using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Entities.Common;

namespace TaskMaster.Entities.Master
{
    public class Comment : AuditEntity
    {
        public Guid Id
        {
            get; set;
        }

        public string Text
        {
            get; set;
        }
    }
}