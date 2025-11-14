using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewTypeCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryIdId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryIdId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CategoryIdId",
                table: "Books",
                newName: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Books",
                newName: "CategoryIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryIdId",
                table: "Books",
                column: "CategoryIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryIdId",
                table: "Books",
                column: "CategoryIdId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
