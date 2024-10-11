using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class AddOfferIdToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "offer_id",
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
                value: new DateTime(2024, 9, 27, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8056));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 9, 28, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8045));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 9, 25, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8052));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 3, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8026));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 9, 26, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8047));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 10, 25, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8050));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 9, 30, 15, 22, 31, 960, DateTimeKind.Local).AddTicks(8057));

            migrationBuilder.CreateIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_activity_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id",
                principalSchema: "activity_seeker",
                principalTable: "activity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_activity_offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 9, 27, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7139));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 9, 28, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7127));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 9, 25, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7135));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 3, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7104));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 9, 26, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 10, 25, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7133));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 9, 30, 14, 26, 23, 140, DateTimeKind.Local).AddTicks(7140));
        }
    }
}
