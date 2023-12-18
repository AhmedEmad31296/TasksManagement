using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksManagement.Migrations
{
    public partial class _16122023_1414 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTasks_AbpUsers_UserId",
                table: "DailyTasks");

            migrationBuilder.DropTable(
                name: "UpdateDailyTasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DailyTasks",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyTasks_UserId",
                table: "DailyTasks",
                newName: "IX_DailyTasks_EmployeeId");

            migrationBuilder.CreateTable(
                name: "UpdatedDailyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalMediaFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyTaskId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdatedDailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdatedDailyTasks_DailyTasks_DailyTaskId",
                        column: x => x.DailyTaskId,
                        principalTable: "DailyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpdatedDailyTasks_DailyTaskId",
                table: "UpdatedDailyTasks",
                column: "DailyTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTasks_AbpUsers_EmployeeId",
                table: "DailyTasks",
                column: "EmployeeId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTasks_AbpUsers_EmployeeId",
                table: "DailyTasks");

            migrationBuilder.DropTable(
                name: "UpdatedDailyTasks");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "DailyTasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyTasks_EmployeeId",
                table: "DailyTasks",
                newName: "IX_DailyTasks_UserId");

            migrationBuilder.CreateTable(
                name: "UpdateDailyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyTaskId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    MediaFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalMediaFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateDailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdateDailyTasks_DailyTasks_DailyTaskId",
                        column: x => x.DailyTaskId,
                        principalTable: "DailyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpdateDailyTasks_DailyTaskId",
                table: "UpdateDailyTasks",
                column: "DailyTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTasks_AbpUsers_UserId",
                table: "DailyTasks",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
