using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_bot_definition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "definition");

            migrationBuilder.CreateTable(
                name: "stateEntity",
                schema: "definition",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transition",
                schema: "definition",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromStateId = table.Column<int>(type: "integer", nullable: false),
                    ToStateId = table.Column<int>(type: "integer", nullable: false),
                    BotStateId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transition", x => x.id);
                    table.ForeignKey(
                        name: "FK_transition_state_BotStateId",
                        column: x => x.BotStateId,
                        principalSchema: "definition",
                        principalTable: "stateEntity",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_transition_state_FromStateId",
                        column: x => x.FromStateId,
                        principalSchema: "definition",
                        principalTable: "stateEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transition_state_ToStateId",
                        column: x => x.ToStateId,
                        principalSchema: "definition",
                        principalTable: "stateEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2025, 2, 18, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2204));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2025, 2, 19, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2190));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2025, 2, 16, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2199));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2025, 2, 24, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2171));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2025, 2, 17, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2193));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2025, 3, 16, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2196));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2025, 2, 21, 19, 21, 34, 454, DateTimeKind.Local).AddTicks(2207));

            migrationBuilder.CreateIndex(
                name: "IX_transition_BotStateId",
                schema: "definition",
                table: "transition",
                column: "BotStateId");

            migrationBuilder.CreateIndex(
                name: "IX_transition_FromStateId",
                schema: "definition",
                table: "transition",
                column: "FromStateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transition_ToStateId",
                schema: "definition",
                table: "transition",
                column: "ToStateId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transition",
                schema: "definition");

            migrationBuilder.DropTable(
                name: "stateEntity",
                schema: "definition");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2025, 2, 4, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(273));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2025, 2, 5, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(259));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2025, 2, 2, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(269));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2025, 2, 10, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(243));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2025, 2, 3, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2025, 3, 2, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(265));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2025, 2, 7, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(277));
        }
    }
}
