using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class AddMainAccountConfigEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainAccountConfigId",
                table: "AccountConfigs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MainAccountConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MainAccountId = table.Column<long>(nullable: false),
                    RegexPattern = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainAccountConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainAccountConfigs_Accounts_MainAccountId",
                        column: x => x.MainAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfigs_MainAccountConfigId",
                table: "AccountConfigs",
                column: "MainAccountConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_MainAccountConfigs_MainAccountId",
                table: "MainAccountConfigs",
                column: "MainAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountConfigs_MainAccountConfigs_MainAccountConfigId",
                table: "AccountConfigs",
                column: "MainAccountConfigId",
                principalTable: "MainAccountConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountConfigs_MainAccountConfigs_MainAccountConfigId",
                table: "AccountConfigs");

            migrationBuilder.DropTable(
                name: "MainAccountConfigs");

            migrationBuilder.DropIndex(
                name: "IX_AccountConfigs_MainAccountConfigId",
                table: "AccountConfigs");

            migrationBuilder.DropColumn(
                name: "MainAccountConfigId",
                table: "AccountConfigs");
        }
    }
}
