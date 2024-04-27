using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "activity_seeker");

            migrationBuilder.CreateTable(
                name: "activity_type",
                schema: "activity_seeker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type_value = table.Column<string>(type: "text", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "activity",
                schema: "activity_seeker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activity_type_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.id);
                    table.ForeignKey(
                        name: "FK_activity_activity_type_activity_type_id",
                        column: x => x.activity_type_id,
                        principalSchema: "activity_seeker",
                        principalTable: "activity_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_activity_type_id",
                schema: "activity_seeker",
                table: "activity",
                column: "activity_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity",
                schema: "activity_seeker");

            migrationBuilder.DropTable(
                name: "activity_type",
                schema: "activity_seeker");
        }
    }
}
