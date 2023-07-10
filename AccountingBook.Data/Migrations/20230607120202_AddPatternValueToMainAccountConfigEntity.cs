using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class AddPatternValueToMainAccountConfigEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainAccountConfigs_Accounts_MainAccountId",
                table: "MainAccountConfigs");

            migrationBuilder.DropIndex(
                name: "IX_MainAccountConfigs_MainAccountId",
                table: "MainAccountConfigs");

            migrationBuilder.DropColumn(
                name: "MainAccountId",
                table: "MainAccountConfigs");

            migrationBuilder.AddColumn<string>(
                name: "PatternValue",
                table: "MainAccountConfigs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatternValue",
                table: "MainAccountConfigs");

            migrationBuilder.AddColumn<long>(
                name: "MainAccountId",
                table: "MainAccountConfigs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MainAccountConfigs_MainAccountId",
                table: "MainAccountConfigs",
                column: "MainAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainAccountConfigs_Accounts_MainAccountId",
                table: "MainAccountConfigs",
                column: "MainAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
