using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleAPI.Migrations
{
    public partial class AddDBToLocal1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }
    }
}
