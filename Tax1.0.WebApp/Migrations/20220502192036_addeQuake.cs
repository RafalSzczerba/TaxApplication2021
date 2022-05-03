using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax1._0.WebApp.Migrations
{
    public partial class addeQuake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModifyQuake",
                table: "Salary",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifyQuake",
                table: "Salary");
        }
    }
}
