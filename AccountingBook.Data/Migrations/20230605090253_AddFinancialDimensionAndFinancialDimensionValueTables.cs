using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class AddFinancialDimensionAndFinancialDimensionValueTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialDimensions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDimensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialDimensionValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DimensionId = table.Column<string>(nullable: true),
                    DimensionTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDimensionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialDimensionValues_FinancialDimensions_DimensionTypeId",
                        column: x => x.DimensionTypeId,
                        principalTable: "FinancialDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDimensionValues_DimensionTypeId",
                table: "FinancialDimensionValues",
                column: "DimensionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialDimensionValues");

            migrationBuilder.DropTable(
                name: "FinancialDimensions");
        }
    }
}
