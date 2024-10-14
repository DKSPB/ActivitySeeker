using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_offer_state : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "offer_state",
                schema: "activity_seeker",
                table: "activity",
                type: "boolean",
                nullable: false);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 10, 16, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2311) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 10, 17, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2297) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 10, 14, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2307) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 10, 22, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2282) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 10, 15, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 11, 14, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2303) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { true, new DateTime(2024, 10, 19, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2314) });
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
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 10, 16, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1686) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 10, 17, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1675) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 10, 14, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1683) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 10, 22, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1661) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 10, 15, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1677) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 11, 14, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1680) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "offer_state", "start_date" },
                values: new object[] { 0, new DateTime(2024, 10, 19, 13, 49, 1, 14, DateTimeKind.Local).AddTicks(1688) });
        }
    }
}
