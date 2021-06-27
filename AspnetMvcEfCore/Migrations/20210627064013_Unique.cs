using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainRegistrarWebApp.Migrations
{
    public partial class Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_Email_Id",
                table: "Users",
                columns: new[] { "Username", "Email", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoughtDomains_Id",
                table: "BoughtDomains",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username_Email_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_BoughtDomains_Id",
                table: "BoughtDomains");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
