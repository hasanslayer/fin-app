using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class RefactorAccountConfigEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRange",
                table: "AccountConfigs");

            migrationBuilder.DropColumn(
                name: "MaxRange",
                table: "AccountConfigs");

            migrationBuilder.DropColumn(
                name: "MinRange",
                table: "AccountConfigs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRange",
                table: "AccountConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxRange",
                table: "AccountConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinRange",
                table: "AccountConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
