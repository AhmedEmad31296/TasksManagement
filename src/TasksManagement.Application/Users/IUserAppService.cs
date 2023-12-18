using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;

using TasksManagement.Helpers;
using TasksManagement.Roles.Dto;
using TasksManagement.Users.Dto;

namespace TasksManagement.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<DatatableFilterdDto<UserDto>> GetPaged(DatatableFilterInput input);
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);
        Task<List<CustomUserDto>> GetEmployees();
        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}
