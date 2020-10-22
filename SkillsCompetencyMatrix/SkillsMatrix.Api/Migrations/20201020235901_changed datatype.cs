using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsMatrix.Api.Migrations
{
    public partial class changeddatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Skills",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Skills",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
