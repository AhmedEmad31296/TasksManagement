using Abp.Application.Services;
using TasksManagement.MultiTenancy.Dto;

namespace TasksManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

