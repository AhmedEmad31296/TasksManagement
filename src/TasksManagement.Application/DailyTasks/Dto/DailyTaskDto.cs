using Abp.Domain.Entities;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using TasksManagement.Common;
using TasksManagement.Helpers;

namespace TasksManagement.DailyTasks.Dto
{
    public class FilterDailyTaskPagedInput : DatatableFilterInput
    {
        public int? TaskStatus { get; set; }

    }
    public class DailyTaskPagedDto : Entity
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DeadLine { get; set; }
        public DailyTaskStatus TaskStatus { get; set; }
        public string EmployeeName { get; set; }
        public int DaysLeft { get; set; }
        public bool HasUpdatedDailyTask { get; set; }
    }
    public class InsertDailyTaskInput
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DeadLine { get; set; }
        public long EmployeeId { get; set; }

    }
    public class UpdateDailyTaskInput : Entity
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DeadLine { get; set; }
        public long EmployeeId { get; set; }
    }
    public class SetDailyTaskStatusCompletedInput : Entity
    {
        public List<IFormFile> Attachments { get; set; }
        public string Note { get; set; }
    }
    public class GetFullInfoDailyTaskDto : Entity
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DeadLine { get; set; }
        public DailyTaskStatus TaskStatus { get; set; }
        public long EmployeeId { get; set; }
    }
    public class UpdatedDailyTaskAttachmentDto
    {
        public string Note { get; set; }
        public string MediaFileUrl { get; set; }
        public string CreatedDate { get; set; }
    }
    public class GetStatisticsDto
    {
        public List<TaskStatusStatisticsDto> TaskStatusStatistics { get; set; }
        public List<EmployeeStatisticsDto> EmployeesStatistics { get; set; }
    }
    public class TaskStatusStatisticsDto
    {
        public DailyTaskStatus TaskStatus { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
    public class EmployeeStatisticsDto
    {
        public string Employee { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
