using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColunmUserIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserIdId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Loans",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_UserIdId",
                table: "Loans",
                newName: "IX_Loans_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Loans",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                newName: "IX_Loans_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserIdId",
                table: "Loans",
                column: "UserIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
