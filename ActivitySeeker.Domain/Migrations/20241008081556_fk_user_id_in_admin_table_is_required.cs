using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class fk_user_id_in_admin_table_is_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admin_user_user_id",
                schema: "activity_seeker",
                table: "admin");

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                schema: "activity_seeker",
                table: "admin",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 10, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7086));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 10, 11, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7073));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 8, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 10, 16, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7051));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 9, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7078));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 8, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7081));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 10, 13, 11, 15, 56, 246, DateTimeKind.Local).AddTicks(7088));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admin_user_user_id",
                schema: "activity_seeker",
                table: "admin");

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                schema: "activity_seeker",
                table: "admin",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.AddForeignKey(
                name: "FK_admin_user_user_id",
                schema: "activity_seeker",
                table: "admin",
                column: "user_id",
                principalSchema: "activity_seeker",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
