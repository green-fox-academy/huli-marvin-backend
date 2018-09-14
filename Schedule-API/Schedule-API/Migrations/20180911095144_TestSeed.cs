using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleAPI.Migrations
{
    public partial class TestSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTemplates",
                columns: table => new
                {
                    EventTemplateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventTemplateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTemplates", x => x.EventTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventType = table.Column<string>(nullable: true),
                    EventTemplateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_EventTemplates_EventTemplateId",
                        column: x => x.EventTemplateId,
                        principalTable: "EventTemplates",
                        principalColumn: "EventTemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EventTemplates",
                columns: new[] { "EventTemplateId", "EventTemplateName" },
                values: new object[] { 1, "t0" });

            migrationBuilder.InsertData(
                table: "EventTemplates",
                columns: new[] { "EventTemplateId", "EventTemplateName" },
                values: new object[] { 2, "t1" });

            migrationBuilder.InsertData(
                table: "EventTemplates",
                columns: new[] { "EventTemplateId", "EventTemplateName" },
                values: new object[] { 3, "t2" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventTemplateId", "EventType" },
                values: new object[,]
                {
                    { 1, 1, "Social" },
                    { 2, 1, "Social" },
                    { 3, 2, "Meeting" },
                    { 5, 2, "Meeting" },
                    { 4, 3, "Social" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTemplateId",
                table: "Events",
                column: "EventTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventTemplates");
        }
    }
}
