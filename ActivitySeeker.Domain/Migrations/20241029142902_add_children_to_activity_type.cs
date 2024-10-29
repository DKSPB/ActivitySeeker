using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_children_to_activity_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                schema: "activity_seeker",
                table: "activity_type",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2024, 10, 31, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3380));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2024, 11, 1, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2024, 10, 29, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3377));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2024, 11, 6, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2024, 10, 30, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3371));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2024, 11, 29, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3374));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2024, 11, 3, 17, 29, 2, 71, DateTimeKind.Local).AddTicks(3382));

            migrationBuilder.CreateIndex(
                name: "IX_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_activity_type_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type",
                column: "parent_id",
                principalSchema: "activity_seeker",
                principalTable: "activity_type",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_type_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type");

            migrationBuilder.DropIndex(
                name: "IX_activity_type_parent_id",
                schema: "activity_seeker",
                table: "activity_type");

            migrationBuilder.DropColumn(
                name: "parent_id",
                schema: "activity_seeker",
                table: "activity_type");

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
        }
    }
}
