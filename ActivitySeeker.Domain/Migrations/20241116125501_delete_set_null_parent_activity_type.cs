using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class delete_set_null_parent_activity_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_type_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 11, 18, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7456));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 11, 19, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 11, 16, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7451));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 11, 24, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7424));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 11, 17, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7443));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 12, 16, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7446));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 11, 21, 15, 55, 0, 949, DateTimeKind.Local).AddTicks(7458));

            migrationBuilder.AddForeignKey(
                name: "FK_activity_type_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type",
                column: "parent_id",
                principalSchema: "activity_seeker",
                principalTable: "activity_type",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_type_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 11, 10, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5477));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 11, 11, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5467));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5474));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 11, 16, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5443));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 11, 9, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5469));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 12, 8, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 11, 13, 16, 44, 22, 591, DateTimeKind.Local).AddTicks(5479));

            migrationBuilder.AddForeignKey(
                name: "FK_activity_type_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type",
                column: "parent_id",
                principalSchema: "activity_seeker",
                principalTable: "activity_type",
                principalColumn: "id");
        }
    }
}
