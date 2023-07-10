using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class AddAccountConfigsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsRange = table.Column<bool>(nullable: false),
                    MinRange = table.Column<int>(nullable: false),
                    MaxRange = table.Column<int>(nullable: false),
                    IsAllowNull = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    FinancialDimensionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountConfigs_FinancialDimensions_FinancialDimensionId",
                        column: x => x.FinancialDimensionId,
                        principalTable: "FinancialDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfigs_FinancialDimensionId",
                table: "AccountConfigs",
                column: "FinancialDimensionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountConfigs");
        }
    }
}
