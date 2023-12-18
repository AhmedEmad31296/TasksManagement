using System.Collections.Generic;
using TasksManagement.Roles.Dto;

namespace TasksManagement.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
