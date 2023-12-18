using Abp.Application.Services;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using TasksManagement.DailyTasks.Dto;
using TasksManagement.Helpers;

namespace TasksManagement.DailyTasks
{
    public interface IDailyTaskAppService : IApplicationService
    {
        Task<DatatableFilterdDto<DailyTaskPagedDto>> GetPaged(FilterDailyTaskPagedInput input);
        Task<string> Insert(InsertDailyTaskInput input);
        Task<string> Update(UpdateDailyTaskInput input);
        Task<string> SetDailyTaskStatusCompleted([FromForm] SetDailyTaskStatusCompletedInput input);
        Task<GetFullInfoDailyTaskDto> Get(int id);
        Task<List<UpdatedDailyTaskAttachmentDto>> GetUpdatedDailyTaskAttachments(int id);
        Task<string> Delete(int id);
        Task<GetStatisticsDto> GetStatistics();
        Task<bool> IsCurrentUserAdmin();

    }
}
