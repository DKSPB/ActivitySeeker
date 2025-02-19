using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class change_transition_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transition_state_FromStateId",
                schema: "definition",
                table: "transition");

            migrationBuilder.DropForeignKey(
                name: "FK_transition_state_ToStateId",
                schema: "definition",
                table: "transition");

            migrationBuilder.RenameColumn(
                name: "ToStateId",
                schema: "definition",
                table: "transition",
                newName: "to_state_id");

            migrationBuilder.RenameColumn(
                name: "FromStateId",
                schema: "definition",
                table: "transition",
                newName: "from_state_id");

            migrationBuilder.RenameIndex(
                name: "IX_transition_ToStateId",
                schema: "definition",
                table: "transition",
                newName: "IX_transition_to_state_id");

            migrationBuilder.RenameIndex(
                name: "IX_transition_FromStateId",
                schema: "definition",
                table: "transition",
                newName: "IX_transition_from_state_id");

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
                name: "FK_transition_state_from_state_id",
                schema: "definition",
                table: "transition",
                column: "from_state_id",
                principalSchema: "definition",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transition_state_to_state_id",
                schema: "definition",
                table: "transition",
                column: "to_state_id",
                principalSchema: "definition",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transition_state_from_state_id",
                schema: "definition",
                table: "transition");

            migrationBuilder.DropForeignKey(
                name: "FK_transition_state_to_state_id",
                schema: "definition",
                table: "transition");

            migrationBuilder.RenameColumn(
                name: "to_state_id",
                schema: "definition",
                table: "transition",
                newName: "ToStateId");

            migrationBuilder.RenameColumn(
                name: "from_state_id",
                schema: "definition",
                table: "transition",
                newName: "FromStateId");

            migrationBuilder.RenameIndex(
                name: "IX_transition_to_state_id",
                schema: "definition",
                table: "transition",
                newName: "IX_transition_ToStateId");

            migrationBuilder.RenameIndex(
                name: "IX_transition_from_state_id",
                schema: "definition",
                table: "transition",
                newName: "IX_transition_FromStateId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_transition_state_FromStateId",
                schema: "definition",
                table: "transition",
                column: "FromStateId",
                principalSchema: "definition",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transition_state_ToStateId",
                schema: "definition",
                table: "transition",
                column: "ToStateId",
                principalSchema: "definition",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
