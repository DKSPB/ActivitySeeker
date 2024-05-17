using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class edit_activity_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 5, 19, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7254));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 5, 20, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7243));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 5, 17, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7251));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 5, 25, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7216));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 5, 18, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7246));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 6, 17, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7248));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 5, 22, 14, 30, 35, 864, DateTimeKind.Local).AddTicks(7256));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                column: "type_name",
                value: "Хобби");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                column: "type_name",
                value: "Мастер-классы");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 5, 19, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1642));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 5, 20, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1626));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 5, 17, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1635));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 5, 25, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1598));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 5, 18, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1629));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 6, 17, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1632));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 5, 22, 11, 37, 43, 165, DateTimeKind.Local).AddTicks(1644));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                column: "type_name",
                value: "События на открытом воздухе");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                column: "type_name",
                value: "События на открытом воздухе");
        }
    }
}
