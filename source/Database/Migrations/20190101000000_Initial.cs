using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DotNetCoreArchitecture.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UsersLogs", "User");
            migrationBuilder.DropTable("Users", "User");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("User");

            migrationBuilder.CreateTable("Users", schema: "User",
                columns: table => new
                {
                    UserId = table.Column<long>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100),
                    Surname = table.Column<string>(maxLength: 200),
                    Email = table.Column<string>(maxLength: 300),
                    Login = table.Column<string>(maxLength: 100),
                    Password = table.Column<string>(maxLength: 500),
                    Roles = table.Column<int>(),
                    Status = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable("UsersLogs", schema: "User",
                columns: table => new
                {
                    UserLogId = table.Column<long>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(),
                    LogType = table.Column<int>(),
                    Content = table.Column<string>(maxLength: 8000, nullable: true),
                    DateTime = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogs", x => x.UserLogId);
                    table.ForeignKey("FK_UsersLogs_Users_UserId",
                        x => x.UserId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "User",
                table: "Users",
                columns: new[] { "UserId", "Email", "Login", "Name", "Password", "Roles", "Status", "Surname" },
                values: new object[] { 1L, "administrator@administrator.com", "admin", "Administrator", "1h0ATANFe6x7kMHo1PURE74WI0ayevUwfK/+Ie+IWX/xWrFWngcVUwL/ewryn38EMVMQBFaNo4SaVwgXaBWnDw==", 3, 1, "Administrator" });

            migrationBuilder.CreateIndex(
                "IX_Users_Email",
                schema: "User",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Users_Login",
                schema: "User",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_UsersLogs_UserId",
                schema: "User",
                table: "UsersLogs",
                column: "UserId");
        }
    }
}
