using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Entities.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskMaster.Helpers.Validation
{
    public class EnumValidator
    {
        public static bool IsTaskPriorityValid(string priorityName)
        {
            foreach (TaskPriority priority in Enum.GetValues(typeof(TaskPriority)))
            {
                if (priority.ToString().Equals(priorityName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTaskStatusValid(string statusName)
        {
            foreach (Entities.Enums.TaskStatus status in Enum.GetValues(typeof(Entities.Enums.TaskStatus)))
            {
                if (status.ToString().Equals(statusName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}