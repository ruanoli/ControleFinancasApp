using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancasApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCategoryTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TRANSACTION",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DATATRANSACTION = table.Column<DateTime>(type: "date", nullable: false),
                    VALUE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TYPE = table.Column<int>(type: "int", nullable: false),
                    CATEGORYID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACTION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TRANSACTION_CATEGORY_CATEGORYID",
                        column: x => x.CATEGORYID,
                        principalTable: "CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRANSACTION_USER_USERID",
                        column: x => x.USERID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTION_CATEGORYID",
                table: "TRANSACTION",
                column: "CATEGORYID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTION_USERID",
                table: "TRANSACTION",
                column: "USERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRANSACTION");

            migrationBuilder.DropTable(
                name: "CATEGORY");
        }
    }
}
