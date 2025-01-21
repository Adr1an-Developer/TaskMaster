using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Entities.DTOs.Reports
{
    public class StatusTaskbyProjectOutputDTO
    {
        public string Id { get; set; } = string.Empty;

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }

        public string FullNameUser
        {
            get; set;
        }
    }
}