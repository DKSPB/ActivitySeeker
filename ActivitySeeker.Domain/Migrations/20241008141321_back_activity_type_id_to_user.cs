using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class back_activity_type_id_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "activity_type_id",
                schema: "activity_seeker",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 10, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1619));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 11, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1608));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 8, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1616));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 16, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1581));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 9, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1611));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1613));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 13, 17, 13, 21, 593, DateTimeKind.Local).AddTicks(1642));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 2, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4186));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 3, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4175));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 9, 30, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4183));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 8, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4151));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 1, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4177));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 10, 30, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 5, 15, 0, 5, 519, DateTimeKind.Local).AddTicks(4188));
        }
    }
}
