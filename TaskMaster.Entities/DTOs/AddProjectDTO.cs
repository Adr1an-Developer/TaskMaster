using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Entities.DTOs
{
    public class AddProjectDTO
    {
        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }
    }
}