using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax1._0.WebApp.Migrations
{
    public partial class newColumnTaxFree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxFreeAvaialable",
                table: "Salary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxFreeAvaialable",
                table: "Salary");
        }
    }
}
