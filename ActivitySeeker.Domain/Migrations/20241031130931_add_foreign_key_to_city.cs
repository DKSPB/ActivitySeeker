using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_foreign_key_to_city : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFilePath = "City.sql";

            if (File.Exists(sqlFilePath))
            {
                var sql = File.ReadAllText(sqlFilePath);
            
                migrationBuilder.Sql(sql);
            }
            
            migrationBuilder.AddColumn<int>(
                name: "city_id",
                schema: "activity_seeker",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "city_id",
                schema: "activity_seeker",
                table: "activity",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 11, 2, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9074));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 11, 3, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9061));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 31, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9069));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 11, 1, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9064));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 30, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9066));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 11, 5, 16, 9, 31, 703, DateTimeKind.Local).AddTicks(9076));

            migrationBuilder.CreateIndex(
                name: "IX_user_city_id",
                schema: "activity_seeker",
                table: "user",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_activity_city_id",
                schema: "activity_seeker",
                table: "activity",
                column: "city_id");

            migrationBuilder.AddForeignKey(
                name: "FK_activity_city_city_id",
                schema: "activity_seeker",
                table: "activity",
                column: "city_id",
                principalSchema: "activity_seeker",
                principalTable: "city",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_city_city_id",
                schema: "activity_seeker",
                table: "user",
                column: "city_id",
                principalSchema: "activity_seeker",
                principalTable: "city",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_city_city_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropForeignKey(
                name: "FK_user_city_city_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_city_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_activity_city_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropColumn(
                name: "city_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "city_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 11, 2, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3505));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 11, 3, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3495));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 31, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3502));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3468));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 11, 1, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3497));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 30, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3500));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 11, 5, 15, 23, 41, 949, DateTimeKind.Local).AddTicks(3507));
        }
    }
}
