using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TasksManagement.EntityFrameworkCore;
using TasksManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TasksManagement.Web.Tests
{
    [DependsOn(
        typeof(TasksManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TasksManagementWebTestModule : AbpModule
    {
        public TasksManagementWebTestModule(TasksManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TasksManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TasksManagementWebMvcModule).Assembly);
        }
    }
}