using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_state_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_activity_type_activity_type_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_activity_type_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "activity_type_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "message_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "search_from",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "search_to",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "state",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.AddColumn<Guid>(
                name: "state_id",
                schema: "activity_seeker",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "state",
                schema: "activity_seeker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    message_id = table.Column<int>(type: "integer", nullable: false),
                    activity_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    search_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    search_to = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                    table.ForeignKey(
                        name: "FK_state_activity_type_activity_type_id",
                        column: x => x.activity_type_id,
                        principalSchema: "activity_seeker",
                        principalTable: "activity_type",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 8, 7, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2362));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 8, 8, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2351));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 8, 5, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2359));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 8, 13, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2329));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 8, 6, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2354));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 9, 5, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2356));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 8, 10, 16, 31, 29, 569, DateTimeKind.Local).AddTicks(2364));

            migrationBuilder.CreateIndex(
                name: "IX_user_state_id",
                schema: "activity_seeker",
                table: "user",
                column: "state_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_state_activity_type_id",
                schema: "activity_seeker",
                table: "state",
                column: "activity_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_state_state_id",
                schema: "activity_seeker",
                table: "user",
                column: "state_id",
                principalSchema: "activity_seeker",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_state_state_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropTable(
                name: "state",
                schema: "activity_seeker");

            migrationBuilder.DropIndex(
                name: "IX_user_state_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "state_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.AddColumn<Guid>(
                name: "activity_type_id",
                schema: "activity_seeker",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "message_id",
                schema: "activity_seeker",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "search_from",
                schema: "activity_seeker",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "search_to",
                schema: "activity_seeker",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "state",
                schema: "activity_seeker",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 8, 2, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2471));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 8, 3, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2454));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 7, 31, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2467));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 8, 8, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2409));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 8, 1, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2459));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 8, 31, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2464));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 8, 5, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2473));

            migrationBuilder.CreateIndex(
                name: "IX_user_activity_type_id",
                schema: "activity_seeker",
                table: "user",
                column: "activity_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_activity_type_activity_type_id",
                schema: "activity_seeker",
                table: "user",
                column: "activity_type_id",
                principalSchema: "activity_seeker",
                principalTable: "activity_type",
                principalColumn: "id");
        }
    }
}
