using Abp.AspNetCore.Mvc.ViewComponents;

namespace TasksManagement.Web.Views
{
    public abstract class TasksManagementViewComponent : AbpViewComponent
    {
        protected TasksManagementViewComponent()
        {
            LocalizationSourceName = TasksManagementConsts.LocalizationSourceName;
        }
    }
}
