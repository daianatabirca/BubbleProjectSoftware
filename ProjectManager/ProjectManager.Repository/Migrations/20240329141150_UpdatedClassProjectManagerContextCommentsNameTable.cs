using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Repository.Migrations
{
    public partial class UpdatedClassProjectManagerContextCommentsNameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProjectObject_ProjectObjectId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectObjectId",
                table: "Comment",
                newName: "IX_Comment_ProjectObjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ProjectObject_ProjectObjectId",
                table: "Comment",
                column: "ProjectObjectId",
                principalTable: "ProjectObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ProjectObject_ProjectObjectId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ProjectObjectId",
                table: "Comments",
                newName: "IX_Comments_ProjectObjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProjectObject_ProjectObjectId",
                table: "Comments",
                column: "ProjectObjectId",
                principalTable: "ProjectObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
