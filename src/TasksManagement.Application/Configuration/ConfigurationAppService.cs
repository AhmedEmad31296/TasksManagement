using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TasksManagement.Configuration.Dto;

namespace TasksManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TasksManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
