using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsMatrix.Api.Migrations
{
    public partial class subCompetency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubCompetencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CompetencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCompetencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubCompetencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    SubCompetencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubCompetencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubCompetencies_SubCompetencies_SubCompetencyId",
                        column: x => x.SubCompetencyId,
                        principalTable: "SubCompetencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubCompetencies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCompetencies_CompetencyId",
                table: "SubCompetencies",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubCompetencies_SubCompetencyId",
                table: "UserSubCompetencies",
                column: "SubCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubCompetencies_UserId",
                table: "UserSubCompetencies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSubCompetencies");

            migrationBuilder.DropTable(
                name: "SubCompetencies");
        }
    }
}
