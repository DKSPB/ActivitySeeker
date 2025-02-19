using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_fk_between_state_and_transition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transition_state_BotStateId",
                schema: "definition",
                table: "transition");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2025, 2, 18, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1379));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2025, 2, 19, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1364));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2025, 2, 16, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1374));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2025, 2, 24, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1344));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2025, 2, 17, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1368));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2025, 3, 16, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1371));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2025, 2, 21, 19, 45, 54, 346, DateTimeKind.Local).AddTicks(1381));

            migrationBuilder.AddForeignKey(
                name: "FK_transition_state_BotStateId",
                schema: "definition",
                table: "transition",
                column: "BotStateId",
                principalSchema: "definition",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transition_state_BotStateId",
                schema: "definition",
                table: "transition");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2025, 2, 18, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8364));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2025, 2, 19, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2025, 2, 16, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8359));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2025, 2, 24, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8330));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2025, 2, 17, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8351));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2025, 3, 16, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8355));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2025, 2, 21, 19, 32, 21, 30, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.AddForeignKey(
                name: "FK_transition_state_BotStateId",
                schema: "definition",
                table: "transition",
                column: "BotStateId",
                principalSchema: "definition",
                principalTable: "state",
                principalColumn: "id");
        }
    }
}
