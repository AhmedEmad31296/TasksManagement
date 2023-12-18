using Abp.Domain.Entities.Auditing;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;

using TasksManagement.Authorization.Users;
using TasksManagement.Common;

namespace TasksManagement.Tasks
{
    [Table("DailyTasks")]
    public class DailyTask : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DeadLine { get; set; }
        public DailyTaskStatus TaskStatus { get; set; }

        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public User Employee { get; set; }
        public ICollection<UpdatedDailyTask> UpdatedDailyTask { get; set; }
    }
}
