using System.Collections.Generic;
using TasksManagement.Roles.Dto;

namespace TasksManagement.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}