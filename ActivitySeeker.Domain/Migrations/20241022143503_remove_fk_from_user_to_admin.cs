using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class remove_fk_from_user_to_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "AdminProfileId",
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
                value: new DateTime(2024, 10, 24, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1775));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 25, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1765));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 22, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1772));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 30, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1737));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 23, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1768));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 22, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 27, 17, 35, 3, 165, DateTimeKind.Local).AddTicks(1777));

            migrationBuilder.CreateIndex(
                name: "IX_user_AdminProfileId",
                schema: "activity_seeker",
                table: "user",
                column: "AdminProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_admin_AdminProfileId",
                schema: "activity_seeker",
                table: "user",
                column: "AdminProfileId",
                principalSchema: "activity_seeker",
                principalTable: "admin",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_admin_AdminProfileId",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_AdminProfileId",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AdminProfileId",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                schema: "activity_seeker",
                table: "admin",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 23, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2537));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 24, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 21, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2534));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 29, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2506));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 22, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2523));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 21, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2526));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 26, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2539));

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
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
