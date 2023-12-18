using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using TasksManagement.Authorization.Roles;
using TasksManagement.Authorization.Users;
using TasksManagement.Configuration;
using TasksManagement.Localization;
using TasksManagement.MultiTenancy;
using TasksManagement.Timing;

namespace TasksManagement
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class TasksManagementCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            TasksManagementLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = TasksManagementConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();

            Configuration.Localization.Languages.Add(new LanguageInfo("en", "En", "famfamfam-flags en"));
            Configuration.Localization.Languages.Add(new LanguageInfo("ar-eg", "العربية", "famfamfam-flags ar", true));

            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = TasksManagementConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = TasksManagementConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TasksManagementCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
