using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TasksManagement.Authorization.Roles;
using TasksManagement.Authorization.Users;
using TasksManagement.MultiTenancy;
using TasksManagement.Tasks;

namespace TasksManagement.EntityFrameworkCore
{
    public class TasksManagementDbContext : AbpZeroDbContext<Tenant, Role, User, TasksManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public TasksManagementDbContext(DbContextOptions<TasksManagementDbContext> options)
            : base(options)
        {
        }
        public DbSet<DailyTask> DailyTasks { get; set; }
        public DbSet<UpdatedDailyTask> UpdateDailyTasks { get; set; }
    }
}
