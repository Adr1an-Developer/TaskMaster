using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Entities.DTOs.Common
{
    public class UserLogged
    {
        public string UserId
        {
            get; set;
        }

        public bool IsManager
        {
            get; set;
        } = false;
    }
}