using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_link_field_to_activity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 7, 28, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5516));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 7, 29, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5506));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 7, 26, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5513));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 8, 3, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5483));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 7, 27, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5508));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 8, 26, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5510));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 7, 31, 15, 12, 30, 64, DateTimeKind.Local).AddTicks(5518));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 7, 27, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9546));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 7, 28, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9536));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 7, 25, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9543));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 8, 2, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9514));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 7, 26, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9538));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 8, 25, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9540));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 7, 30, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9548));
        }
    }
}
