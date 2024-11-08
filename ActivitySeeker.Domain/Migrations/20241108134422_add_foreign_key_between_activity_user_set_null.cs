using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_foreign_key_between_activity_user_set_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_activity_offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "activity_seeker",
                table: "activity",
                newName: "id");

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

            migrationBuilder.CreateIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_activity_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id",
                principalSchema: "activity_seeker",
                principalTable: "activity",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "activity_seeker",
                table: "activity",
                newName: "Id");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 11, 5, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1913));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 11, 6, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1899));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 11, 3, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1908));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 11, 11, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1881));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 11, 4, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1901));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 12, 3, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1904));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 14, 29, 57, 921, DateTimeKind.Local).AddTicks(1917));

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
    }
}
