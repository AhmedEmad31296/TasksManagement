using System.Threading.Tasks;
using TasksManagement.Configuration.Dto;

namespace TasksManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
