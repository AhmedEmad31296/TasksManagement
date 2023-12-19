using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using TasksManagement.Authorization;
using TasksManagement.Controllers;
using TasksManagement.Roles;
using TasksManagement.Web.Models.Roles;
using TasksManagement.Helpers;
using TasksManagement.Roles.Dto;
using Abp.Authorization;
using System;

namespace TasksManagement.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class RolesController : TasksManagementControllerBase
    {
        private readonly IRoleAppService _roleAppService;

        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        public async Task<IActionResult> Index()
        {
            var permissions = (await _roleAppService.GetAllPermissions()).Items;
            var model = new RoleListViewModel
            {
                Permissions = permissions
            };

            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetPaged()
        {
            DatatableFilterInput input = GetDatatableFilterInput();
            DatatableFilterdDto<RoleDto> result = await _roleAppService.GetPaged(input);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] CreateRoleDto input)
        {
            return Json(await _roleAppService.CreateAsync(input));
        }
        [HttpPost]
        public async Task<JsonResult> Update([FromBody] RoleDto input)
        {
            return Json(await _roleAppService.UpdateAsync(input));
        }
        [HttpGet]
        public async Task<ActionResult> EditModal(int id)
        {
            var output = await _roleAppService.GetRoleForEdit(new EntityDto(id));
            var model = ObjectMapper.Map<EditRoleModalViewModel>(output);

            return PartialView("_EditModal", model);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var input = new EntityDto<int> { Id = id };
            try
            {
                await _roleAppService.DeleteAsync(input);
                return Json(new { success = true, message = L("DeletedSuccessfully") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = L("RoleDeleteWarningMessage") + ex.Message });
            }
        }
    }
}
