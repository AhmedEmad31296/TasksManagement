using Abp.Authorization;
using TasksManagement.Authorization.Roles;
using TasksManagement.Authorization.Users;

namespace TasksManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
