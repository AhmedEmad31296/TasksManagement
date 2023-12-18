using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TasksManagement.Authorization;

namespace TasksManagement
{
    [DependsOn(
        typeof(TasksManagementCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TasksManagementApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TasksManagementAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TasksManagementApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
