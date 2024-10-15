using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class remove_link_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.DropColumn(
                name: "link",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.RenameColumn(
                name: "offer_state",
                schema: "activity_seeker",
                table: "activity",
                newName: "is_published");

            migrationBuilder.AddColumn<string>(
                name: "link_description",
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
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол", new DateTime(2024, 10, 17, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5157) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", new DateTime(2024, 10, 18, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5140) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", new DateTime(2024, 10, 15, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5153) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе", new DateTime(2024, 10, 23, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5110) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", new DateTime(2024, 10, 16, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5144) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды", new DateTime(2024, 11, 15, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5148) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "link_description", "start_date" },
                values: new object[] { "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!", new DateTime(2024, 10, 20, 20, 10, 30, 380, DateTimeKind.Local).AddTicks(5160) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link_description",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.RenameColumn(
                name: "is_published",
                schema: "activity_seeker",
                table: "activity",
                newName: "offer_state");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link",
                schema: "activity_seeker",
                table: "activity",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол", new DateTime(2024, 10, 16, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2311) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!", new DateTime(2024, 10, 17, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2297) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь", new DateTime(2024, 10, 14, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2307) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе", new DateTime(2024, 10, 22, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2282) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками", new DateTime(2024, 10, 15, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды", new DateTime(2024, 11, 14, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2303) });

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "Id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                columns: new[] { "description", "start_date" },
                values: new object[] { "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!", new DateTime(2024, 10, 19, 14, 55, 24, 803, DateTimeKind.Local).AddTicks(2314) });
        }
    }
}
