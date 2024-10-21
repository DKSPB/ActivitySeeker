using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "activity_seeker");

            migrationBuilder.CreateTable(
                name: "activity_type",
                schema: "activity_seeker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "activity",
                schema: "activity_seeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    link_description = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<byte[]>(type: "bytea", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    activity_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_published = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_activity_activity_type_activity_type_id",
                        column: x => x.activity_type_id,
                        principalSchema: "activity_seeker",
                        principalTable: "activity_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "activity_seeker",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chat_id = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    message_id = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    activity_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    search_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    search_to = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    activity_result = table.Column<string>(type: "text", nullable: false),
                    offer_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_activity_offer_id",
                        column: x => x.offer_id,
                        principalSchema: "activity_seeker",
                        principalTable: "activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_activity_type_activity_type_id",
                        column: x => x.activity_type_id,
                        principalSchema: "activity_seeker",
                        principalTable: "activity_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admin",
                schema: "activity_seeker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    hashed_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.id);
                    table.ForeignKey(
                        name: "FK_admin_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "activity_seeker",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "activity_seeker",
                table: "activity_type",
                columns: new[] { "id", "type_name" },
                values: new object[,]
                {
                    { new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), "Хобби" },
                    { new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), "События на открытом воздухе" },
                    { new Guid("fd689706-6407-4665-a982-e39e4db3c608"), "Мастер-классы" }
                });

            migrationBuilder.InsertData(
                schema: "activity_seeker",
                table: "activity",
                columns: new[] { "Id", "activity_type_id", "image", "is_published", "link_description", "start_date" },
                values: new object[,]
                {
                    { new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), null, true, "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол", new DateTime(2024, 10, 23, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2537) },
                    { new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"), new Guid("fd689706-6407-4665-a982-e39e4db3c608"), null, true, "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", new DateTime(2024, 10, 24, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2520) },
                    { new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"), new Guid("fd689706-6407-4665-a982-e39e4db3c608"), null, true, "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", new DateTime(2024, 10, 21, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2534) },
                    { new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), null, true, "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе", new DateTime(2024, 10, 29, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2506) },
                    { new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"), new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), null, true, "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", new DateTime(2024, 10, 22, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2523) },
                    { new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"), new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), null, true, "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды", new DateTime(2024, 11, 21, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2526) },
                    { new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), null, true, "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!", new DateTime(2024, 10, 26, 12, 29, 23, 136, DateTimeKind.Local).AddTicks(2539) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_activity_type_id",
                schema: "activity_seeker",
                table: "activity",
                column: "activity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_user_id",
                schema: "activity_seeker",
                table: "admin",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_activity_type_id",
                schema: "activity_seeker",
                table: "user",
                column: "activity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_offer_id",
                schema: "activity_seeker",
                table: "user",
                column: "offer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin",
                schema: "activity_seeker");

            migrationBuilder.DropTable(
                name: "user",
                schema: "activity_seeker");

            migrationBuilder.DropTable(
                name: "activity",
                schema: "activity_seeker");

            migrationBuilder.DropTable(
                name: "activity_type",
                schema: "activity_seeker");
        }
    }
}
