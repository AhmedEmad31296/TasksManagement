using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using TasksManagement.Helpers;
using TasksManagement.DailyTasks.Dto;
using TasksManagement.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using TasksManagement.Heplers;
using Abp.Extensions;
using TasksManagement.Authorization.Users;
using TasksManagement.Authorization.Roles;
using Microsoft.AspNetCore.Identity;
using Abp.Runtime.Session;
using Abp.Authorization.Users;

namespace TasksManagement.DailyTasks
{
    [AbpAuthorize]
    public class DailyTaskAppService : TasksManagementAppServiceBase, IDailyTaskAppService
    {
        private readonly IRepository<DailyTask> _DailyTaskRepository;
        private readonly IRepository<UpdatedDailyTask> _UpdatedDailyTaskRepository;
        private readonly IHostEnvironment _HostEnvironment;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        readonly string BaseUrl = "";

        public DailyTaskAppService(IRepository<DailyTask> DailyTaskRepository
            , IRepository<UpdatedDailyTask> updatedDailyTaskRepository
            , IHostEnvironment hostEnvironment
            , IHttpContextAccessor httpContextAccessor)
        {
            _DailyTaskRepository = DailyTaskRepository;
            _UpdatedDailyTaskRepository = updatedDailyTaskRepository;
            _HostEnvironment = hostEnvironment;
            _HttpContextAccessor = httpContextAccessor;
            BaseUrl = $"{this._HttpContextAccessor.HttpContext.Request.Scheme}://{this._HttpContextAccessor.HttpContext.Request.Host}{this._HttpContextAccessor.HttpContext.Request.PathBase}";
        }

        public async Task<DatatableFilterdDto<DailyTaskPagedDto>> GetPaged(FilterDailyTaskPagedInput input)
        {
            await SetInProgressAfterStartEntryDate();
            bool isCurrentUserAdmin = await IsCurrentUserAdmin();

            IQueryable<DailyTask> query = _DailyTaskRepository.GetAll().Where(e => !e.IsDeleted)
                ;

            int totalCount = await query.CountAsync();

            query = query
                .WhereIf(!isCurrentUserAdmin, e => e.EmployeeId == AbpSession.UserId)
                .WhereIf(!string.IsNullOrEmpty(input.SearchTerm), b => b.Name.ToLower().Contains(input.SearchTerm)
                                                                || (int)b.TaskStatus == input.TaskStatus)
                ;


            int recordsFiltered = await query.CountAsync();

            // Apply sorting
            if (!string.IsNullOrEmpty(input.SortColumn) && !string.IsNullOrEmpty(input.SortDirection))
            {
                query = query.OrderBy(string.Concat(input.SortColumn, " ", input.SortDirection));
            }

            // Pagination
            List<DailyTaskPagedDto> dailyTasks = await query
                .Select(b => new DailyTaskPagedDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    TaskStatus = b.TaskStatus,
                    Comment = b.Comment,
                    DeadLine = b.DeadLine,
                    EntryDate = b.EntryDate,
                    EmployeeName = b.Employee.FullName,
                    DaysLeft = (int)(b.DeadLine - b.EntryDate).TotalDays,
                    HasUpdatedDailyTask = b.UpdatedDailyTask.Any()
                })
                //.Skip((input.Page - 1) * input.PageSize)
                //.Take(input.PageSize)
                .Page(input.Page, input.PageSize)
                .ToListAsync();

            return new DatatableFilterdDto<DailyTaskPagedDto>
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = totalCount,
                AaData = dailyTasks,
                Draw = input.Draw
            };

        }
        public async Task<string> Insert(InsertDailyTaskInput input)
        {
            bool dailyTaskIsExisting = await _DailyTaskRepository.GetAll().Where(b => b.Name.Equals(input.Name)).AnyAsync();
            if (dailyTaskIsExisting)
                throw new UserFriendlyException(L("DailyTask.IsAlreadyExisting"));

            DailyTask dailyTask = new()
            {
                Name = input.Name,
                Comment = input.Comment,
                EntryDate = input.EntryDate,
                DeadLine = input.DeadLine,
                EmployeeId = input.EmployeeId,
                TaskStatus = input.EntryDate < DateTime.Now ? Common.DailyTaskStatus.NotStarted : Common.DailyTaskStatus.InProgress
            };

            await _DailyTaskRepository.InsertAsync(dailyTask);
            return L("SavedSuccessfully");

        }
        public async Task<string> Update(UpdateDailyTaskInput input)
        {
            bool dailyTaskIsExisting = await _DailyTaskRepository.GetAll().Where(b => b.Id != input.Id && b.Name.Equals(input.Name)).AnyAsync();
            if (dailyTaskIsExisting)
                throw new UserFriendlyException(L("DailyTask.IsAlreadyExisting"));

            DailyTask dailyTask = await _DailyTaskRepository.GetAll().Where(b => b.Id == input.Id).FirstOrDefaultAsync();

            dailyTask.Name = input.Name;
            dailyTask.Comment = input.Comment;
            dailyTask.EntryDate = input.EntryDate;
            dailyTask.DeadLine = input.DeadLine;
            dailyTask.EmployeeId = input.EmployeeId;
            dailyTask.TaskStatus = input.EntryDate < DateTime.Now ? Common.DailyTaskStatus.NotStarted : Common.DailyTaskStatus.InProgress;
            await _DailyTaskRepository.UpdateAsync(dailyTask);

            return L("UpdatedSuccessfully");
        }
        public async Task<string> SetDailyTaskStatusCompleted([FromForm] SetDailyTaskStatusCompletedInput input)
        {
            DailyTask dailyTask = await _DailyTaskRepository
                .GetAll()
                .Where(x => x.Id == input.Id)
                .FirstOrDefaultAsync();

            dailyTask.TaskStatus = Common.DailyTaskStatus.Completed;
            await _DailyTaskRepository.UpdateAsync(dailyTask);

            if (input.Attachments != null)
            {
                List<UpdatedDailyTask> updatedDailyTasks = new();
                var rootPath = _HostEnvironment.ContentRootPath;
                foreach (IFormFile attachment in input.Attachments)
                {
                    var uploadedMediaFile = await MediaFileService.UploadMediaFileAsync(attachment, rootPath + TasksManagementConsts.DailyTaskAttachmentPath.UploadPath);
                    if (uploadedMediaFile != null)
                    {
                        UpdatedDailyTask updatedDailyTask = new()
                        {
                            Note = input.Note,
                            MediaFileName = uploadedMediaFile.FileName,
                            OriginalMediaFileName = uploadedMediaFile.OriginalFileName,
                            DailyTaskId = dailyTask.Id
                        };
                        updatedDailyTasks.Add(updatedDailyTask);
                        _UpdatedDailyTaskRepository.Insert(updatedDailyTask);
                    }
                }
            }
            return L("UpdatedSuccessfully");
        }
        public async Task<GetFullInfoDailyTaskDto> Get(int id)
        {
            DailyTask dailyTask = await _DailyTaskRepository.GetAll()
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync() ?? throw new UserFriendlyException(L("DailyTask.IsNotExisting"));

            GetFullInfoDailyTaskDto entity = await _DailyTaskRepository.GetAll()
                                                                .Where(e => e.Id == id)
                                                                .Select(e => new GetFullInfoDailyTaskDto
                                                                {
                                                                    Id = e.Id,
                                                                    Name = e.Name,
                                                                    Comment = e.Comment,
                                                                    EntryDate = e.EntryDate,
                                                                    DeadLine = e.DeadLine,
                                                                    EmployeeId = e.EmployeeId,
                                                                    TaskStatus = e.TaskStatus,
                                                                }).FirstOrDefaultAsync();
            return entity;
        }
        public async Task<List<UpdatedDailyTaskAttachmentDto>> GetUpdatedDailyTaskAttachments(int id)
        {
            var attachments = await _UpdatedDailyTaskRepository.GetAll()
                 .Where(x => x.DailyTaskId == id)
                 .Select(a => new UpdatedDailyTaskAttachmentDto
                 {
                     Note = a.Note,
                     CreatedDate = a.CreationTime.ToShortDateString(),
                     MediaFileUrl = !a.MediaFileName.IsNullOrEmpty() ? BaseUrl + TasksManagementConsts.DailyTaskAttachmentPath.FolderPath + a.MediaFileName : "",
                 }).ToListAsync();
            return attachments;
        }
        public async Task<string> Delete(int id)
        {
            DailyTask dailyTask = await _DailyTaskRepository.GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync() ?? throw new UserFriendlyException(L("DailyTask.IsNotExisting"));

            await _DailyTaskRepository.HardDeleteAsync(dailyTask);

            return L("DeletedSuccessfully");
        }
        public async Task<GetStatisticsDto> GetStatistics()
        {
            GetStatisticsDto statistics = new()
            {
                TaskStatusStatistics = await GetDailyTasksStatistics(),
            };
            return statistics;
        }
        public async Task<bool> IsCurrentUserAdmin()
        {
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());

            if (user != null)
            {
                var roles = await UserManager.GetRolesAsync(user);
                return roles.Contains("Admin");
            }

            return false;
        }

        async Task<List<GetTaskStatusStatisticsDto>> GetDailyTasksStatistics()
        {
            int totalTasksCount = await _DailyTaskRepository.GetAll().CountAsync(e => !e.IsDeleted);
            bool isCurrentUserAdmin = await IsCurrentUserAdmin();

            List<GetTaskStatusStatisticsDto> statistics = await _DailyTaskRepository.GetAll()
                .Where(e => !e.IsDeleted)
                .WhereIf(!isCurrentUserAdmin, x => x.EmployeeId == AbpSession.UserId)
                .GroupBy(e => e.TaskStatus)
                .Select(e => new GetTaskStatusStatisticsDto
                {
                    TaskStatus = e.Key,
                    Total = e.Count(),
                    Percentage = ((double)e.Count() / totalTasksCount) * 100
                })
                .OrderBy(c => c.TaskStatus)
                .ToListAsync();
            return statistics;
        }
        async Task SetInProgressAfterStartEntryDate()
        {
            List<DailyTask> dailyTasks = await _DailyTaskRepository
                .GetAll()
                .Where(x => DateTime.Now.Date >= x.EntryDate.Date && x.TaskStatus == Common.DailyTaskStatus.NotStarted)
                .ToListAsync();

            if (dailyTasks.Count > 0)
                foreach (var dt in dailyTasks)
                {
                    dt.TaskStatus = Common.DailyTaskStatus.InProgress;
                    await _DailyTaskRepository.UpdateAsync(dt);
                }
        }

    }
}
