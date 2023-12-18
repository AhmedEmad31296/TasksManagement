using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Linq;
using System.Threading.Tasks;

using TasksManagement.Controllers;
using TasksManagement.DailyTasks;
using TasksManagement.DailyTasks.Dto;
using TasksManagement.Helpers;
using TasksManagement.Users;

namespace TasksManagement.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DailyTaskController : TasksManagementControllerBase
    {
        private readonly IDailyTaskAppService _IDailyTaskAppService;
        private readonly IUserAppService _IUserAppService;
        public DailyTaskController(IDailyTaskAppService IDailyTaskAppService, IUserAppService iUserAppService)
        {
            _IDailyTaskAppService = IDailyTaskAppService;
            _IUserAppService = iUserAppService;
        }
        public async Task<ActionResult> Index()
        {
            var employees = await _IUserAppService.GetEmployees();
            ViewData["Employees"] = new SelectList(employees, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetPaged()
        {
            int draw = Convert.ToInt32(HttpContext.Request.Form["draw"].FirstOrDefault());
            int start = Convert.ToInt32(HttpContext.Request.Form["start"].FirstOrDefault());
            int length = Convert.ToInt32(HttpContext.Request.Form["length"].FirstOrDefault());
            string sortColumn = HttpContext.Request.Form[$"columns[{HttpContext.Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            string sortColumnDir = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            string searchTerm = HttpContext.Request.Form["search[value]"].FirstOrDefault();

            FilterDailyTaskPagedInput input = new()
            {
                Draw = draw,
                Page = start / length + 1,
                PageSize = length,
                SortColumn = sortColumn,
                SortDirection = sortColumnDir,
                SearchTerm = searchTerm
            };

            DatatableFilterdDto<DailyTaskPagedDto> result = await _IDailyTaskAppService.GetPaged(input);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await _IDailyTaskAppService.Delete(id));
        }
        [HttpGet]
        public async Task<ActionResult> EditModal(int id)
        {
            var result = await _IDailyTaskAppService.Get(id);
            var employees = await _IUserAppService.GetEmployees();
            ViewData["Employees"] = new SelectList(employees, "Id", "Name", result.EmployeeId);
            return PartialView("_EditModal", result);
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployeeAttachments(int id)
        {
            var result = await _IDailyTaskAppService.GetUpdatedDailyTaskAttachments(id);
            return PartialView("_EmployeeAttachmentsModal", result);
        }
        [HttpGet]
        public ActionResult SetCompleteModal(int id)
        {
            return PartialView("_SetCompleteModal", id);
        }
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] InsertDailyTaskInput input)
        {
            return Json(await _IDailyTaskAppService.Insert(input));
        }
        [HttpPost]
        public async Task<JsonResult> Update([FromBody] UpdateDailyTaskInput input)
        {
            return Json(await _IDailyTaskAppService.Update(input));
        }
        [HttpPost]
        public async Task<JsonResult> SetDailyTaskStatusCompleted(SetDailyTaskStatusCompletedInput input)
        {
            return Json(await _IDailyTaskAppService.SetDailyTaskStatusCompleted(input));
        }
    }
}
