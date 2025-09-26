using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class rewrite_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_city_city_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropForeignKey(
                name: "FK_user_activity_offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_activity_type_activity_type_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_admin_AdminProfileId",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_city_city_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_activity_type_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_AdminProfileId",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_city_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_activity_city_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropColumn(
                name: "AdminProfileId",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "activity_format",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "activity_result",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "activity_type_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "chat_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "city_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "message_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "offer_id",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "search_from",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "search_to",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "state",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "username",
                schema: "activity_seeker",
                table: "user");

            migrationBuilder.DropColumn(
                name: "image_path",
                schema: "activity_seeker",
                table: "activity_type");

            migrationBuilder.DropColumn(
                name: "tg_message_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.RenameColumn(
                name: "link_description",
                schema: "activity_seeker",
                table: "activity",
                newName: "description");

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                schema: "activity_seeker",
                table: "activity",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "timezone",
                schema: "activity_seeker",
                table: "activity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                schema: "activity_seeker",
                table: "activity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_activity_user_id",
                schema: "activity_seeker",
                table: "activity",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_activity_user_user_id",
                schema: "activity_seeker",
                table: "activity",
                column: "user_id",
                principalSchema: "activity_seeker",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activity_user_user_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropIndex(
                name: "IX_activity_user_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropColumn(
                name: "end_date",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropColumn(
                name: "timezone",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "activity_seeker",
                table: "activity",
                newName: "link_description");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminProfileId",
                schema: "activity_seeker",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "activity_format",
                schema: "activity_seeker",
                table: "user",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "activity_result",
                schema: "activity_seeker",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "activity_type_id",
                schema: "activity_seeker",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "chat_id",
                schema: "activity_seeker",
                table: "user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "city_id",
                schema: "activity_seeker",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "message_id",
                schema: "activity_seeker",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "offer_id",
                schema: "activity_seeker",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "search_from",
                schema: "activity_seeker",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "search_to",
                schema: "activity_seeker",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "state",
                schema: "activity_seeker",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "username",
                schema: "activity_seeker",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image_path",
                schema: "activity_seeker",
                table: "activity_type",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tg_message_id",
                schema: "activity_seeker",
                table: "activity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_activity_type_id",
                schema: "activity_seeker",
                table: "user",
                column: "activity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_AdminProfileId",
                schema: "activity_seeker",
                table: "user",
                column: "AdminProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_user_city_id",
                schema: "activity_seeker",
                table: "user",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id",
                unique: true);

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
                name: "FK_user_activity_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id",
                principalSchema: "activity_seeker",
                principalTable: "activity",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_activity_type_activity_type_id",
                schema: "activity_seeker",
                table: "user",
                column: "activity_type_id",
                principalSchema: "activity_seeker",
                principalTable: "activity_type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_admin_AdminProfileId",
                schema: "activity_seeker",
                table: "user",
                column: "AdminProfileId",
                principalSchema: "activity_seeker",
                principalTable: "admin",
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
    }
}
