using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax1._0.WebApp.Migrations
{
    public partial class added1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxFree",
                table: "Salary");

            migrationBuilder.AlterColumn<int>(
                name: "Tax",
                table: "Salary",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Tax",
                table: "Salary",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxFree",
                table: "Salary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
