using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using TasksManagement.Controllers;
using TasksManagement.DailyTasks;
using TasksManagement.DailyTasks.Dto;
using System.Threading.Tasks;

namespace TasksManagement.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : TasksManagementControllerBase
    {
        private readonly IDailyTaskAppService _IDailyTaskAppService;

        public HomeController(IDailyTaskAppService IDailyTaskAppService)
        {
            _IDailyTaskAppService = IDailyTaskAppService;
        }
        public async Task<ActionResult> Index()
        {
            GetStatisticsDto statistics = await _IDailyTaskAppService.GetStatistics();
            return View(statistics);
        }
    }
}
