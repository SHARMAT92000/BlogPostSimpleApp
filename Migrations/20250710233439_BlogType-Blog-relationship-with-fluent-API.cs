using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPostApplication.Migrations
{
    /// <inheritdoc />
    public partial class BlogTypeBlogrelationshipwithfluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogTypeId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogTypeId",
                table: "Blogs",
                column: "BlogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogType_BlogTypeId",
                table: "Blogs",
                column: "BlogTypeId",
                principalTable: "BlogType",
                principalColumn: "BlogTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogType_BlogTypeId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogTypeId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogTypeId",
                table: "Blogs");
        }
    }
}
