using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class delete_field_name_fron_activity_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол", new DateTime(2024, 8, 14, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9844) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", new DateTime(2024, 8, 15, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9833) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", new DateTime(2024, 8, 12, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9840) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе", new DateTime(2024, 8, 20, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9811) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", new DateTime(2024, 8, 13, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9835) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды", new DateTime(2024, 9, 12, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9838) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!", new DateTime(2024, 8, 17, 14, 43, 31, 459, DateTimeKind.Local).AddTicks(9845) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Все желающие, присоединяйтесь к нашей команде для игры в футбол", "Футбол в Мурино", new DateTime(2024, 8, 2, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2471) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", "Игра в настолку Бункер", new DateTime(2024, 8, 3, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2454) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", "Вархаммер 40000", new DateTime(2024, 7, 31, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2467) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Приглашаем всех присоединиться к тренировкам на открытом воздухе", "Тренеровки на открытом воздухе", new DateTime(2024, 8, 8, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2409) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", "Мастер-класс по изготовлению свечи", new DateTime(2024, 8, 1, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2459) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Приглашаем на наш мастер-класс по изготовлению глиняной посуды", "Мастер-класс по изготовлению глиняной посуды", new DateTime(2024, 8, 31, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2464) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "description", "name", "start_date" },
                values: new object[] { "Fitness Hause Мурино проводит соревнования по настольному теннису!", "Соревнования по настольному теннису", new DateTime(2024, 8, 5, 17, 14, 18, 506, DateTimeKind.Local).AddTicks(2473) });
        }
    }
}
