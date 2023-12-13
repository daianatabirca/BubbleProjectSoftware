using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SprintNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Assignee = table.Column<string>(type: "TEXT", nullable: true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectObject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectObject_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CommentArea = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ProjectObjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ProjectObject_ProjectObjectId",
                        column: x => x.ProjectObjectId,
                        principalTable: "ProjectObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectObjectHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ProjectObjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectObjectHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectObjectHistory_ProjectObject_ProjectObjectId",
                        column: x => x.ProjectObjectId,
                        principalTable: "ProjectObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectObjectRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RelationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectObjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    RelatedObjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectObjectRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectObjectRelation_ProjectObject_ProjectObjectId",
                        column: x => x.ProjectObjectId,
                        principalTable: "ProjectObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectObjectRelation_ProjectObject_RelatedObjectId",
                        column: x => x.RelatedObjectId,
                        principalTable: "ProjectObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectObjectRelation_RelationType_RelationTypeId",
                        column: x => x.RelationTypeId,
                        principalTable: "RelationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectObjectId",
                table: "Comments",
                column: "ProjectObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectObject_ProjectId",
                table: "ProjectObject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectObject_StatusId",
                table: "ProjectObject",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectObjectHistory_ProjectObjectId",
                table: "ProjectObjectHistory",
                column: "ProjectObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectObjectRelation_ProjectObjectId",
                table: "ProjectObjectRelation",
                column: "ProjectObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectObjectRelation_RelatedObjectId",
                table: "ProjectObjectRelation",
                column: "RelatedObjectId");

            migrationBuilder.CreateIndex(
                name: "UniqueConstraintRelation_Index",
                table: "ProjectObjectRelation",
                columns: new[] { "RelationTypeId", "ProjectObjectId", "RelatedObjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ProjectObjectHistory");

            migrationBuilder.DropTable(
                name: "ProjectObjectRelation");

            migrationBuilder.DropTable(
                name: "ProjectObject");

            migrationBuilder.DropTable(
                name: "RelationType");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
