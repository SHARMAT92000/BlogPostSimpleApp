using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPostApplication.Migrations
{
    /// <inheritdoc />
    public partial class PostTypeSchemaChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostTypeId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.PostTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTypeId",
                table: "Posts",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostType_PostTypeId",
                table: "Posts",
                column: "PostTypeId",
                principalTable: "PostType",
                principalColumn: "PostTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostType_PostTypeId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostType");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostTypeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostTypeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "Blogs");
        }
    }
}
