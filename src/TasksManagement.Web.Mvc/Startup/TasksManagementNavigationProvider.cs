using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;

using TasksManagement.Authorization;

namespace TasksManagement.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class TasksManagementNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                        )
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TasksManagementConsts.LocalizationSourceName);
        }
    }
}