using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TasksManagement.Common;

namespace TasksManagement.Tasks
{
    [Table("UpdatedDailyTasks")]
    public class UpdatedDailyTask : FullAuditedEntity
    {
        public string Note { get; set; }
        public string MediaFileName { get; set; }
        public string OriginalMediaFileName { get; set; }

        [ForeignKey("DailyTask")]
        public int DailyTaskId { get; set; }
        public DailyTask DailyTask { get; set; }

    }
}
