using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Etalent_C__.Net_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountHolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AccountHolders_AccountHolderId",
                        column: x => x.AccountHolderId,
                        principalTable: "AccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawals_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountHolders",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "FirstName", "IdNumber", "LastName", "Mobile" },
                values: new object[,]
                {
                    { 1, "123 Main Street, Johannesburg", new DateTime(1985, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "8505201234088", "Doe", "+27831234567" },
                    { 2, "456 Oak Avenue, Cape Town", new DateTime(1990, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "9009155432084", "Smith", "+27839876543" },
                    { 3, "789 Pine Road, Durban", new DateTime(1978, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.ngubane@example.com", "Peter", "7802025677081", "Ngubane", "+27831231234" }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountHolderId", "AccountNumber", "AvailableBalance", "Name", "Status", "Type" },
                values: new object[,]
                {
                    { 1, 1, 1001001001, 2500.00m, "John's Cheque Account", 0, 0 },
                    { 2, 1, 1001001002, 7800.50m, "John's Savings", 0, 1 },
                    { 3, 2, 1001002001, 15000.00m, "Jane's Savings", 1, 1 },
                    { 4, 2, 1001002002, 20000.00m, "Jane's Fixed Deposit", 0, 2 },
                    { 5, 3, 1001003001, 500.00m, "Peter's Cheque Account", 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountHolderId",
                table: "BankAccounts",
                column: "AccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_BankAccountId",
                table: "Withdrawals",
                column: "BankAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Withdrawals");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "AccountHolders");
        }
    }
}
