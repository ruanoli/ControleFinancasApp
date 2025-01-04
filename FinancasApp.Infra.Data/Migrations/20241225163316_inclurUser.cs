using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancasApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class inclurUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTION_USER_USERID",
                table: "TRANSACTION");

            migrationBuilder.AddColumn<Guid>(
                name: "USERID",
                table: "CATEGORY",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_USERID",
                table: "CATEGORY",
                column: "USERID");

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORY_USER_USERID",
                table: "CATEGORY",
                column: "USERID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTION_USER_USERID",
                table: "TRANSACTION",
                column: "USERID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORY_USER_USERID",
                table: "CATEGORY");

            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTION_USER_USERID",
                table: "TRANSACTION");

            migrationBuilder.DropIndex(
                name: "IX_CATEGORY_USERID",
                table: "CATEGORY");

            migrationBuilder.DropColumn(
                name: "USERID",
                table: "CATEGORY");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTION_USER_USERID",
                table: "TRANSACTION",
                column: "USERID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
