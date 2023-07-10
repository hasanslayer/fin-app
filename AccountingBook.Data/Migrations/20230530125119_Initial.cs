using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(maxLength: 100, nullable: false),
                    AccountCode = table.Column<int>(maxLength: 5, nullable: false),
                    DrOrCrSide = table.Column<int>(nullable: false),
                    AccountType = table.Column<int>(nullable: false),
                    ParentAccountId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    CompanyCode = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerLines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrCr = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    GeneralLedgerHeaderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerLines_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerLines_GeneralLedgerHeaders_GeneralLedgerHeaderId",
                        column: x => x.GeneralLedgerHeaderId,
                        principalTable: "GeneralLedgerHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Posted = table.Column<bool>(nullable: false),
                    GeneralLedgerHeaderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryHeaders_GeneralLedgerHeaders_GeneralLedgerHeaderId",
                        column: x => x.GeneralLedgerHeaderId,
                        principalTable: "GeneralLedgerHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrCr = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    JournalEntryHeaderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_JournalEntryHeaders_JournalEntryHeaderId",
                        column: x => x.JournalEntryHeaderId,
                        principalTable: "JournalEntryHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountCode", "AccountName", "AccountType", "DrOrCrSide", "ParentAccountId" },
                values: new object[,]
                {
                    { 1L, 10000, "Assets", 1, 1, null },
                    { 2L, 20000, "Liabilities", 2, 2, null },
                    { 3L, 30000, "Equity", 3, 2, null },
                    { 4L, 40000, "Revenue", 4, 2, null },
                    { 5L, 50000, "Expense", 5, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CompanyCode", "CompanyName", "ShortName" },
                values: new object[] { 1L, "C001", "hexquote.com", "hexquote" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountCode", "AccountName", "AccountType", "DrOrCrSide", "ParentAccountId" },
                values: new object[,]
                {
                    { 6L, 10111, "Regular Checking Account", 1, 1, 1L },
                    { 30L, 50500, "Purchase price Variance", 5, 1, 5L },
                    { 29L, 50400, "Purchase Discounts", 5, 1, 5L },
                    { 28L, 50300, "Cost of Goods Sold", 5, 1, 5L },
                    { 27L, 50200, "Purchase A/C", 5, 1, 5L },
                    { 26L, 50101, "Salary Expenses", 5, 1, 5L },
                    { 25L, 40500, "Shipping and Handling", 4, 2, 4L },
                    { 24L, 40200, "Sales Discounts", 4, 2, 4L },
                    { 23L, 40100, "Sales A/C", 4, 2, 4L },
                    { 22L, 30500, "Accumulated Losses", 3, 2, 3L },
                    { 21L, 30400, "Accumulated Profits", 3, 2, 3L },
                    { 20L, 30300, "Retained Surplus", 3, 2, 3L },
                    { 31L, 50600, "Other Expenses", 5, 1, 5L },
                    { 19L, 30200, "Capital Surplus", 3, 2, 3L },
                    { 17L, 20300, "Sales Tax", 2, 2, 2L },
                    { 16L, 20202, "Wages Payable", 2, 2, 2L },
                    { 15L, 20120, "Customer Advances", 2, 2, 2L },
                    { 14L, 20110, "Account Payable", 2, 2, 2L },
                    { 13L, 10810, "Goods Received Clearing Account", 1, 1, 1L },
                    { 12L, 10800, "Inventory", 1, 1, 1L },
                    { 11L, 10150, "Employee Advances", 1, 1, 1L },
                    { 10L, 10140, "Prepaid Expenses", 1, 1, 1L },
                    { 9L, 10120, "Accounts Receivable", 1, 1, 1L },
                    { 8L, 10113, "Cash in Hand A/C", 1, 1, 1L },
                    { 7L, 10112, "Savings Account", 1, 1, 1L },
                    { 18L, 30100, "Member Capital", 3, 2, 3L },
                    { 32L, 50700, "Purchase Tax", 5, 1, 5L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountCode",
                table: "Accounts",
                column: "AccountCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLines_AccountId",
                table: "GeneralLedgerLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLines_GeneralLedgerHeaderId",
                table: "GeneralLedgerLines",
                column: "GeneralLedgerHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryHeaders_GeneralLedgerHeaderId",
                table: "JournalEntryHeaders",
                column: "GeneralLedgerHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_AccountId",
                table: "JournalEntryLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_JournalEntryHeaderId",
                table: "JournalEntryLines",
                column: "JournalEntryHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "GeneralLedgerLines");

            migrationBuilder.DropTable(
                name: "JournalEntryLines");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "JournalEntryHeaders");

            migrationBuilder.DropTable(
                name: "GeneralLedgerHeaders");
        }
    }
}
