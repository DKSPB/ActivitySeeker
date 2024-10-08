using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_navigation_property_from_user_to_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_admin",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "activity_seeker",
                table: "admin",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "activity_seeker",
                table: "admin",
                newName: "login");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                schema: "activity_seeker",
                table: "admin",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 10, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3904));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 11, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3894));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 8, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3901));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 16, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3870));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 9, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3897));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3899));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 13, 11, 4, 16, 884, DateTimeKind.Local).AddTicks(3906));

            migrationBuilder.CreateIndex(
                name: "IX_admin_user_id",
                schema: "activity_seeker",
                table: "admin",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_admin_user_user_id",
                schema: "activity_seeker",
                table: "admin",
                column: "user_id",
                principalSchema: "activity_seeker",
                principalTable: "user",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admin_user_user_id",
                schema: "activity_seeker",
                table: "admin");

            migrationBuilder.DropIndex(
                name: "IX_admin_user_id",
                schema: "activity_seeker",
                table: "admin");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "activity_seeker",
                table: "admin");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "activity_seeker",
                table: "admin",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "login",
                schema: "activity_seeker",
                table: "admin",
                newName: "username");

            migrationBuilder.AddColumn<bool>(
                name: "is_admin",
                schema: "activity_seeker",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 6, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 7, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 4, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7178));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 12, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7146));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 5, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7173));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 4, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7175));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 9, 15, 12, 29, 883, DateTimeKind.Local).AddTicks(7184));
        }
    }
}
