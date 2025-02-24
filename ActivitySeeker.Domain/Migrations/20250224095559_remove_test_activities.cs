using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class remove_test_activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "activity_seeker",
                table: "activity",
                columns: new[] { "id", "activity_type_id", "city_id", "image", "is_online", "is_published", "link_description", "start_date", "tg_message_id" },
                values: new object[,]
                {
                    { new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), null, null, false, true, "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол", new DateTime(2025, 2, 4, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(273), null },
                    { new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"), new Guid("fd689706-6407-4665-a982-e39e4db3c608"), null, null, false, true, "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", new DateTime(2025, 2, 5, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(259), null },
                    { new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"), new Guid("fd689706-6407-4665-a982-e39e4db3c608"), null, null, false, true, "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", new DateTime(2025, 2, 2, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(269), null },
                    { new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), null, null, false, true, "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе", new DateTime(2025, 2, 10, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(243), null },
                    { new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"), new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), null, null, false, true, "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", new DateTime(2025, 2, 3, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(261), null },
                    { new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"), new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), null, null, false, true, "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды", new DateTime(2025, 3, 2, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(265), null },
                    { new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), null, null, false, true, "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!", new DateTime(2025, 2, 7, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(277), null }
                });
        }
    }
}
