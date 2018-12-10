using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Portfolio_Strona.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactMe",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutMe1 = table.Column<string>(nullable: true),
                    AboutMe2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    GitHub = table.Column<string>(nullable: true),
                    LinkedIn = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMe", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Literatures",
                columns: table => new
                {
                    LiteratureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Authors = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Literatures", x => x.LiteratureID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BackColor = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    GitHubUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    WebUrl = table.Column<string>(nullable: true),
                    WorkLogUrl = table.Column<string>(nullable: true),
                    YouTubeUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    TechnologyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    PictureLink = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.TechnologyID);
                });

            migrationBuilder.CreateTable(
                name: "LiteraturesTech",
                columns: table => new
                {
                    LiteratureID = table.Column<int>(nullable: false),
                    TechnologyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteraturesTech", x => new { x.LiteratureID, x.TechnologyID });
                    table.ForeignKey(
                        name: "FK_LiteraturesTech_Literatures_LiteratureID",
                        column: x => x.LiteratureID,
                        principalTable: "Literatures",
                        principalColumn: "LiteratureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiteraturesTech_Technologies_TechnologyID",
                        column: x => x.TechnologyID,
                        principalTable: "Technologies",
                        principalColumn: "TechnologyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechProjects",
                columns: table => new
                {
                    TechnologyID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechProjects", x => new { x.TechnologyID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_TechProjects_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechProjects_Technologies_TechnologyID",
                        column: x => x.TechnologyID,
                        principalTable: "Technologies",
                        principalColumn: "TechnologyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiteraturesTech_TechnologyID",
                table: "LiteraturesTech",
                column: "TechnologyID");

            migrationBuilder.CreateIndex(
                name: "IX_TechProjects_ProjectID",
                table: "TechProjects",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMe");

            migrationBuilder.DropTable(
                name: "LiteraturesTech");

            migrationBuilder.DropTable(
                name: "TechProjects");

            migrationBuilder.DropTable(
                name: "Literatures");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technologies");
        }
    }
}
