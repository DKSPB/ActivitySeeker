﻿// <auto-generated />
using System;
using ActivitySeeker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    [DbContext(typeof(ActivitySeekerContext))]
    [Migration("20240725110828_add_is_admin_flag_to_user")]
    partial class add_is_admin_flag_to_user
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("activity_type_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.ToTable("activity", "activity_seeker");

                    b.HasData(
                        new
                        {
                            Id = new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                            ActivityTypeId = new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                            Description = "Приглашаем всех присоединиться к тренировкам на открытом воздухе",
                            Name = "Тренеровки на открытом воздухе",
                            StartDate = new DateTime(2024, 8, 2, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9514)
                        },
                        new
                        {
                            Id = new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                            ActivityTypeId = new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                            Description = "Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!",
                            Name = "Игра в настолку Бункер",
                            StartDate = new DateTime(2024, 7, 28, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9536)
                        },
                        new
                        {
                            Id = new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                            ActivityTypeId = new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                            Description = "Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками",
                            Name = "Мастер-класс по изготовлению свечи",
                            StartDate = new DateTime(2024, 7, 26, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9538)
                        },
                        new
                        {
                            Id = new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                            ActivityTypeId = new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                            Description = "Приглашаем на наш мастер-класс по изготовлению глиняной посуды",
                            Name = "Мастер-класс по изготовлению глиняной посуды",
                            StartDate = new DateTime(2024, 8, 25, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9540)
                        },
                        new
                        {
                            Id = new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                            ActivityTypeId = new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                            Description = "Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь",
                            Name = "Вархаммер 40000",
                            StartDate = new DateTime(2024, 7, 25, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9543)
                        },
                        new
                        {
                            Id = new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                            ActivityTypeId = new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                            Description = "Все желающие, присоединяйтесь к нашей команде для игры в футбол",
                            Name = "Футбол в Мурино",
                            StartDate = new DateTime(2024, 7, 27, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9546)
                        },
                        new
                        {
                            Id = new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                            ActivityTypeId = new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                            Description = "Fitness Hause Мурино проводит соревнования по настольному теннису!",
                            Name = "Соревнования по настольному теннису",
                            StartDate = new DateTime(2024, 7, 30, 14, 8, 28, 460, DateTimeKind.Local).AddTicks(9548)
                        });
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.ActivityType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id");

                    b.ToTable("activity_type", "activity_seeker");

                    b.HasData(
                        new
                        {
                            Id = new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                            TypeName = "События на открытом воздухе"
                        },
                        new
                        {
                            Id = new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                            TypeName = "Хобби"
                        },
                        new
                        {
                            Id = new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                            TypeName = "Мастер-классы"
                        });
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ActivityResult")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("activity_result");

                    b.Property<Guid?>("ActivityTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("activity_type_id");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint")
                        .HasColumnName("chat_id");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_admin");

                    b.Property<int>("MessageId")
                        .HasColumnType("integer")
                        .HasColumnName("message_id");

                    b.Property<DateTime>("SearchFrom")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("search_from");

                    b.Property<DateTime>("SearchTo")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("search_to");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.ToTable("user", "activity_seeker");
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.Activity", b =>
                {
                    b.HasOne("ActivitySeeker.Domain.Entities.ActivityType", "ActivityType")
                        .WithMany("Activities")
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityType");
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.User", b =>
                {
                    b.HasOne("ActivitySeeker.Domain.Entities.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId");

                    b.Navigation("ActivityType");
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.ActivityType", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
