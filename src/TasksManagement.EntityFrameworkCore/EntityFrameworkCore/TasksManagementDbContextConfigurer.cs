using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TasksManagement.EntityFrameworkCore
{
    public static class TasksManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TasksManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TasksManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
