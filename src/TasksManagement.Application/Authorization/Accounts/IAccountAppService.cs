using System.Threading.Tasks;
using Abp.Application.Services;
using TasksManagement.Authorization.Accounts.Dto;

namespace TasksManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
