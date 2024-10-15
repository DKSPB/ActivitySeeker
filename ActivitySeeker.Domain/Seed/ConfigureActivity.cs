using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivitySeeker.Domain.Seed;

public class ConfigureActivity: IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(k => k.Id);
        List<Activity> activities = new()
        {
            new Activity
            {
                Id = Guid.Parse("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                ActivityTypeId = Guid.Parse("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                LinkOrDescription =  "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе",
                StartDate = DateTime.Now.AddDays(8),
                IsPublished = true
            },
            new Activity
            {
                Id = Guid.Parse("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                ActivityTypeId = Guid.Parse("fd689706-6407-4665-a982-e39e4db3c608"),
                LinkOrDescription = "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!",
                StartDate = DateTime.Now.AddDays(3),
                IsPublished = true
            },

            new Activity()
            {
                Id = Guid.Parse("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                ActivityTypeId = Guid.Parse("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                LinkOrDescription = "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками",
                StartDate = DateTime.Now.AddDays(1),
                IsPublished = true
            },

            new Activity()
            {
                Id = Guid.Parse("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                ActivityTypeId = Guid.Parse("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                LinkOrDescription = "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды",
                StartDate = DateTime.Now.AddMonths(1),
                IsPublished = true
            },

            new Activity()
            {
                Id = Guid.Parse("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                ActivityTypeId = Guid.Parse("fd689706-6407-4665-a982-e39e4db3c608"),
                LinkOrDescription = "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь",
                StartDate = DateTime.Now,
                IsPublished = true
            },

            new Activity
            {
                Id = Guid.Parse("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                ActivityTypeId = Guid.Parse("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                LinkOrDescription =  "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол",
                StartDate = DateTime.Now.AddDays(2),
                IsPublished = true
            },

            new Activity()
            {
                Id = Guid.Parse("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                ActivityTypeId = Guid.Parse("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                LinkOrDescription = "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!",
                StartDate = DateTime.Now.AddDays(5),
                IsPublished = true
            }
        };

        builder.HasData(activities);
    }
}