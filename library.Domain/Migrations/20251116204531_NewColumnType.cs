using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Loans_LoanModelId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_UserId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Books_LoanModelId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LoanModelId",
                table: "Books");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "BookIds",
                table: "Loans",
                type: "uuid[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookIds",
                table: "Loans");

            migrationBuilder.AddColumn<Guid>(
                name: "LoanModelId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
