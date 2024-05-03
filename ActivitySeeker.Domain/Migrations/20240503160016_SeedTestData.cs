using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class SeedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                schema: "activity_seeker",
                table: "activity",
                newName: "Id");

            migrationBuilder.InsertData(
                schema: "activity_seeker",
                table: "activity_type",
                columns: new[] { "id", "type_name", "type_value" },
                values: new object[,]
                {
                    { new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), "MasterClass", "Мастер-классы" },
                    { new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), "OpenAir", "События на открытом воздухе" },
                    { new Guid("fd689706-6407-4665-a982-e39e4db3c608"), "BoardGames", "Настольные игры" }
                });

            migrationBuilder.InsertData(
                schema: "activity_seeker",
                table: "activity",
                columns: new[] { "Id", "activity_type_id", "description", "name", "start_date" },
                values: new object[,]
                {
                    { new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), "Все желающие, присоединяйтесь к нашей команде для игры в футбол", "Футбол в Мурино", new DateTime(2024, 5, 5, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9561) },
                    { new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"), new Guid("fd689706-6407-4665-a982-e39e4db3c608"), "Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", "Игра в настолку Бункер", new DateTime(2024, 5, 6, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9539) },
                    { new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"), new Guid("fd689706-6407-4665-a982-e39e4db3c608"), "Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", "Вархаммер 40000", new DateTime(2024, 5, 3, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9556) },
                    { new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), "Приглашаем всех присоединиться к тренировкам на открытом воздухе", "Тренеровки на открытом воздухе", new DateTime(2024, 5, 11, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9526) },
                    { new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"), new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), "Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", "Мастер-класс по изготовлению свечи", new DateTime(2024, 5, 4, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9542) },
                    { new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"), new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"), "Приглашаем на наш мастер-класс по изготовлению глиняной посуды", "Мастер-класс по изготовлению глиняной посуды", new DateTime(2024, 6, 3, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9545) },
                    { new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"), new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"), "Fitness Hause Мурино проводит соревнования по настольному теннису!", "Соревнования по настольному теннису", new DateTime(2024, 5, 8, 19, 0, 16, 477, DateTimeKind.Local).AddTicks(9563) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"));

            migrationBuilder.DeleteData(
                schema: "activity_seeker",
                table: "activity_type",
                keyColumn: "id",
                keyValue: new Guid("fd689706-6407-4665-a982-e39e4db3c608"));

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "activity_seeker",
                table: "activity",
                newName: "id");
        }
    }
}
