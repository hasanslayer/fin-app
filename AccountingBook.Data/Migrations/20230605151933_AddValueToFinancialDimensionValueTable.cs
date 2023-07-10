using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class AddValueToFinancialDimensionValueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FinancialDimensionValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "FinancialDimensionValues");
        }
    }
}
