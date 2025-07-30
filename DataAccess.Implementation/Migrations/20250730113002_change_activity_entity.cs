using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class change_activity_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.AlterColumn<bool>(
                name: "is_published",
                schema: "activity_seeker",
                table: "activity",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_path",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_path",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.AlterColumn<bool>(
                name: "is_published",
                schema: "activity_seeker",
                table: "activity",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                schema: "activity_seeker",
                table: "activity",
                type: "bytea",
                nullable: true);
        }
    }
}
