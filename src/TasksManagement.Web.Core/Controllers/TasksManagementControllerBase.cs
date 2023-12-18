using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

using System;
using System.Linq;

using TasksManagement.Helpers;

namespace TasksManagement.Controllers
{
    public abstract class TasksManagementControllerBase: AbpController
    {
        protected TasksManagementControllerBase()
        {
            LocalizationSourceName = TasksManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        protected DatatableFilterInput GetDatatableFilterInput()
        {
            int draw = Convert.ToInt32(HttpContext.Request.Form["draw"].FirstOrDefault());
            int start = Convert.ToInt32(HttpContext.Request.Form["start"].FirstOrDefault());
            int length = Convert.ToInt32(HttpContext.Request.Form["length"].FirstOrDefault());
            string sortColumn = HttpContext.Request.Form[$"columns[{HttpContext.Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            string sortColumnDir = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            string searchTerm = HttpContext.Request.Form["search[value]"].FirstOrDefault();

            return new DatatableFilterInput
            {
                Draw = draw,
                Page = start / length + 1,
                PageSize = length,
                SortColumn = sortColumn,
                SortDirection = sortColumnDir,
                SearchTerm = searchTerm
            };
        }
    }
}
