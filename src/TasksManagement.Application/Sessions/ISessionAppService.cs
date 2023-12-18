using System.Threading.Tasks;
using Abp.Application.Services;
using TasksManagement.Sessions.Dto;

namespace TasksManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
