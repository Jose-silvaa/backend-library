using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddBooksToList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookIdId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookIdId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BookIdId",
                table: "Loans");

            migrationBuilder.AddColumn<Guid>(
                name: "LoanModelId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_LoanModelId",
                table: "Books",
                column: "LoanModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Loans_LoanModelId",
                table: "Books",
                column: "LoanModelId",
                principalTable: "Loans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Loans_LoanModelId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LoanModelId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LoanModelId",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookIdId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookIdId",
                table: "Loans",
                column: "BookIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookIdId",
                table: "Loans",
                column: "BookIdId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
