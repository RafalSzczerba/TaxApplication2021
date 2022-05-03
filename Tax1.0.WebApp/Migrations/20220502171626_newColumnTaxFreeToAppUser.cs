using Microsoft.EntityFrameworkCore.Migrations;

namespace Tax1._0.WebApp.Migrations
{
    public partial class newColumnTaxFreeToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxFreeAvaialable",
                table: "Salary");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxFreeAvaialbe",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxFreeAvaialbe",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxFreeAvaialable",
                table: "Salary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
