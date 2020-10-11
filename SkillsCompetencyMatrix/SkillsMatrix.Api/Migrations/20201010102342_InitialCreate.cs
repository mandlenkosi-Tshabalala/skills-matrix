using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsMatrix.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetencyCatagories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyCatagories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertiseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IdNumber = table.Column<long>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    UserLastName = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CatagoryId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competencies_CompetencyCatagories_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "CompetencyCatagories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expertises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ExpertiseId = table.Column<int>(nullable: false),
                    CatagoryId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expertises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expertises_ExpertiseCategories_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "ExpertiseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhysicalCode = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    PhysicalAddressLine1 = table.Column<string>(nullable: true),
                    PostalAddressLine1 = table.Column<string>(nullable: true),
                    PhysicalAddressLine2 = table.Column<string>(nullable: false),
                    PostalAddressLine2 = table.Column<string>(nullable: true),
                    PhysicalAddressLine3 = table.Column<string>(nullable: true),
                    PostalAddressLine3 = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Institution = table.Column<string>(nullable: false),
                    FieldOfStudy = table.Column<string>(nullable: false),
                    QualificationStartDate = table.Column<string>(nullable: false),
                    QualificationEndDate = table.Column<string>(nullable: false),
                    QualificationLevel = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CurrentlyWorking = table.Column<bool>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    RoleDescription = table.Column<string>(nullable: false),
                    PersonID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentHistories_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalMemberships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Membership = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionalMemberships_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PersonID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_CatagoryId",
                table: "Competencies",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_PersonID",
                table: "Educations",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistories_PersonID",
                table: "EmploymentHistories",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Expertises_CatagoryId",
                table: "Expertises",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalMemberships_PersonID",
                table: "ProfessionalMemberships",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_References_PersonID",
                table: "References",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PersonID",
                table: "Skills",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "EmploymentHistories");

            migrationBuilder.DropTable(
                name: "Expertises");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "ProfessionalMemberships");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CompetencyCatagories");

            migrationBuilder.DropTable(
                name: "ExpertiseCategories");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
