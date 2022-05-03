using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax1._0.WebApp.Migrations
{
    public partial class added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tax",
                table: "Salary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxFree",
                table: "Salary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "TaxFree",
                table: "Salary");
        }
    }
}
