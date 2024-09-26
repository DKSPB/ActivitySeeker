using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class ActivityOfferState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "offer_state",
                schema: "activity_seeker",
                table: "activity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 9, 27, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7139) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 9, 28, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7127) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 9, 25, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7135) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 10, 3, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7104) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 9, 26, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7130) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 10, 25, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7133) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 3, new DateTime(2024, 9, 30, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7140) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "offer_state",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 8, 14, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 8, 15, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9833));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 8, 12, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 8, 20, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9811));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 8, 13, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9835));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 9, 12, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9838));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 8, 17, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9845));
        }
    }
}
