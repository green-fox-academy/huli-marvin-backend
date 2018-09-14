using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberService.Migrations
{
    public partial class ToLocale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Late = table.Column<int>(nullable: false),
                    DayOff = table.Column<int>(nullable: false),
                    SickVerified = table.Column<int>(nullable: false),
                    SickUnverified = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZipNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cohorts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cohorts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    GitHubUser = table.Column<string>(nullable: true),
                    SlackUser = table.Column<string>(nullable: true),
                    LinkedIn = table.Column<string>(nullable: true),
                    Education = table.Column<int>(nullable: false),
                    IsSigned = table.Column<bool>(nullable: false),
                    Payment = table.Column<int>(nullable: false),
                    Phase = table.Column<int>(nullable: false),
                    AttendanceInfoId = table.Column<long>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    CohortApprenticeId = table.Column<long>(nullable: true),
                    ClassApprenticeId = table.Column<long>(nullable: true),
                    TeamApprenticeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Attendances_AttendanceInfoId",
                        column: x => x.AttendanceInfoId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Profiles_Cohorts_CohortApprenticeId",
                        column: x => x.CohortApprenticeId,
                        principalTable: "Cohorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Profiles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CalendarId = table.Column<long>(nullable: false),
                    SlackChannelId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: true),
                    CohortId = table.Column<long>(nullable: true),
                    ClassLeadId = table.Column<long>(nullable: true),
                    ClassAdminId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Profiles_ClassAdminId",
                        column: x => x.ClassAdminId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Profiles_ClassLeadId",
                        column: x => x.ClassLeadId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobHistories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobHistories_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    ProductOwnerId = table.Column<long>(nullable: true),
                    ScrumMasterId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Profiles_ProductOwnerId",
                        column: x => x.ProductOwnerId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Profiles_ScrumMasterId",
                        column: x => x.ScrumMasterId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassProfile",
                columns: table => new
                {
                    ClassId = table.Column<long>(nullable: false),
                    ProfileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassProfile", x => new { x.ClassId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ClassProfile_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassProfile_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "DayOff", "Late", "SickUnverified", "SickVerified" },
                values: new object[,]
                {
                    { 1L, 0, 1, 1, 0 },
                    { 2L, 0, 14, 2, 2 },
                    { 3L, 2, 5, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "CalendarId", "ClassAdminId", "ClassLeadId", "CohortId", "Color", "CourseId", "Name", "SlackChannelId", "Status" },
                values: new object[,]
                {
                    { 1L, 2L, null, null, null, "red", null, "Ocelot", 7L, 0 },
                    { 2L, 1L, null, null, null, "blue", null, "Secret", 2L, 2 },
                    { 3L, 3L, null, null, null, "green", null, "Raptor", 5L, 1 },
                    { 4L, 4L, null, null, null, "yellow", null, "Lasers", 1L, 0 },
                    { 5L, 5L, null, null, null, "purple", null, "BadBoi", 6L, 0 },
                    { 6L, 7L, null, null, null, "orange", null, "Seagal", 4L, 1 },
                    { 7L, 6L, null, null, null, "black", null, "Teapot", 3L, 2 }
                });

            migrationBuilder.InsertData(
                table: "Cohorts",
                columns: new[] { "Id", "DepartmentId", "FinishDate", "Name", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2017, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ace", new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2L, null, new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alopex", new DateTime(2017, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3L, null, new DateTime(2018, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macrotis", new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4L, null, new DateTime(2018, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fulvipes", new DateTime(2018, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "DepartmentId", "Name", "Status" },
                values: new object[,]
                {
                    { 3L, null, "Super Mommies", 2 },
                    { 2L, null, "Accenture Girls", 1 },
                    { 1L, null, "Standard", 0 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Address", "Country", "Email", "Name", "PhoneNumber", "ZipNumber" },
                values: new object[,]
                {
                    { 1L, "Andrassy 66.", "HU", "hr@gf.com", "HR", "36701234567", "1000" },
                    { 2L, "Andrassy 66.", "HU", "mentors@gf.com", "Mentors", "36701234568", "1000" },
                    { 3L, "Andrassy 66.", "HU", "partnermgmt@gf.com", "Partner Management", "36701234569", "1000" }
                });

            migrationBuilder.InsertData(
                table: "JobHistories",
                columns: new[] { "Id", "Name", "ProfileId" },
                values: new object[,]
                {
                    { 1L, "Economist", null },
                    { 2L, "Teacher", null },
                    { 3L, "Psychologist", null }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "AttendanceInfoId", "ClassApprenticeId", "CohortApprenticeId", "DateOfBirth", "Education", "Email", "Gender", "GitHubUser", "IsSigned", "Level", "LinkedIn", "Name", "Payment", "Phase", "PhoneNumber", "Picture", "ProjectId", "SlackUser", "TeamApprenticeId" },
                values: new object[,]
                {
                    { 1L, null, null, null, new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "x", 0, "x", true, 3, "x", "Adam", 1, 0, "x", "x", null, "x", null },
                    { 2L, null, null, null, new DateTime(2017, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "x", 0, "x", true, 0, "x", "Eva", 1, 0, "x", "x", null, "x", null },
                    { 3L, null, null, null, new DateTime(2017, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "x", 1, "x", true, 1, "x", "Janos", 0, 1, "x", "x", null, "x", null }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "DepartmentId", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, null, "Csharp", "Marvin" },
                    { 2L, null, "JAVA", "Szera" },
                    { 3L, null, "Python, DevOps", "Malachite" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name", "ProductOwnerId", "ProjectId", "ScrumMasterId" },
                values: new object[,]
                {
                    { 1L, "Amazonite", null, null, null },
                    { 2L, "Malachite", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ClassProfile",
                columns: new[] { "ClassId", "ProfileId" },
                values: new object[] { 2L, 1L });

            migrationBuilder.InsertData(
                table: "ClassProfile",
                columns: new[] { "ClassId", "ProfileId" },
                values: new object[] { 1L, 2L });

            migrationBuilder.InsertData(
                table: "ClassProfile",
                columns: new[] { "ClassId", "ProfileId" },
                values: new object[] { 3L, 3L });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassAdminId",
                table: "Classes",
                column: "ClassAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassLeadId",
                table: "Classes",
                column: "ClassLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CohortId",
                table: "Classes",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassProfile_ProfileId",
                table: "ClassProfile",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_DepartmentId",
                table: "Cohorts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistories_ProfileId",
                table: "JobHistories",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AttendanceInfoId",
                table: "Profiles",
                column: "AttendanceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ClassApprenticeId",
                table: "Profiles",
                column: "ClassApprenticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CohortApprenticeId",
                table: "Profiles",
                column: "CohortApprenticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ProjectId",
                table: "Profiles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_TeamApprenticeId",
                table: "Profiles",
                column: "TeamApprenticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ProductOwnerId",
                table: "Teams",
                column: "ProductOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ProjectId",
                table: "Teams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ScrumMasterId",
                table: "Teams",
                column: "ScrumMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Classes_ClassApprenticeId",
                table: "Profiles",
                column: "ClassApprenticeId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Teams_TeamApprenticeId",
                table: "Profiles",
                column: "TeamApprenticeId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Profiles_ClassAdminId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Profiles_ClassLeadId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Profiles_ProductOwnerId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Profiles_ScrumMasterId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "ClassProfile");

            migrationBuilder.DropTable(
                name: "JobHistories");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Cohorts");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
