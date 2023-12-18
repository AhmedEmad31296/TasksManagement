using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace TasksManagement.Web.Views
{
    public abstract class TasksManagementRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected TasksManagementRazorPage()
        {
            LocalizationSourceName = TasksManagementConsts.LocalizationSourceName;
        }
    }
}
