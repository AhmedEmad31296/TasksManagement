using System.Collections.Generic;
using TasksManagement.Roles.Dto;

namespace TasksManagement.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
