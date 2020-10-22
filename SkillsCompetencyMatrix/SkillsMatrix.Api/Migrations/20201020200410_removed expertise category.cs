using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsMatrix.Api.Migrations
{
    public partial class removedexpertisecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expertises_ExpertiseCategories_CatagoryId",
                table: "Expertises");

            migrationBuilder.DropTable(
                name: "ExpertiseCategories");

            migrationBuilder.DropIndex(
                name: "IX_Expertises_CatagoryId",
                table: "Expertises");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "Expertises");

            migrationBuilder.DropColumn(
                name: "ExpertiseId",
                table: "Expertises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "Expertises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpertiseId",
                table: "Expertises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExpertiseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expertises_CatagoryId",
                table: "Expertises",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expertises_ExpertiseCategories_CatagoryId",
                table: "Expertises",
                column: "CatagoryId",
                principalTable: "ExpertiseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
