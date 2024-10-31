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
    [Migration("20241031115533_add_city_table")]
    partial class add_city_table
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

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea")
                        .HasColumnName("image");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean")
                        .HasColumnName("is_published");

                    b.Property<string>("LinkOrDescription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("link_description");

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
                            IsPublished = true,
                            LinkOrDescription = "Тренеровки на открытом воздухе. Приглашаем всех присоединиться к тренировкам на открытом воздухе",
                            StartDate = new DateTime(2024, 11, 8, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9377)
                        },
                        new
                        {
                            Id = new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                            ActivityTypeId = new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                            IsPublished = true,
                            LinkOrDescription = "Игра в настолку Бункер. Магазин Слон в посудной лавке организует прекрасный вечер за игрой в Бункер! присоединяйся!",
                            StartDate = new DateTime(2024, 11, 3, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9400)
                        },
                        new
                        {
                            Id = new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                            ActivityTypeId = new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                            IsPublished = true,
                            LinkOrDescription = "Мастер-класс по изготовлению свечи. Магазин Слон в посудной лавке приглашает всех желающих посетить мастер-класс по изготовлению аромо-свечи своими руками",
                            StartDate = new DateTime(2024, 11, 1, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9403)
                        },
                        new
                        {
                            Id = new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                            ActivityTypeId = new Guid("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                            IsPublished = true,
                            LinkOrDescription = "Мастер-класс по изготовлению глиняной посуды. Приглашаем на наш мастер-класс по изготовлению глиняной посуды",
                            StartDate = new DateTime(2024, 11, 30, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9405)
                        },
                        new
                        {
                            Id = new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                            ActivityTypeId = new Guid("fd689706-6407-4665-a982-e39e4db3c608"),
                            IsPublished = true,
                            LinkOrDescription = "Вархаммер 40000. Магазин Hobby Games организует соревнование по игре в вархаммер! присоединяйтесь",
                            StartDate = new DateTime(2024, 10, 31, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9408)
                        },
                        new
                        {
                            Id = new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                            ActivityTypeId = new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                            IsPublished = true,
                            LinkOrDescription = "Футбол в Мурино. Все желающие, присоединяйтесь к нашей команде для игры в футбол",
                            StartDate = new DateTime(2024, 11, 2, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9411)
                        },
                        new
                        {
                            Id = new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                            ActivityTypeId = new Guid("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                            IsPublished = true,
                            LinkOrDescription = "Соревнования по настольному теннису. Fitness House Мурино проводит соревнования по настольному теннису!",
                            StartDate = new DateTime(2024, 11, 5, 14, 55, 33, 53, DateTimeKind.Local).AddTicks(9413)
                        });
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.ActivityType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

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

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.HasKey("Id");

                    b.ToTable("admin", "activity_seeker");
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("city", "activity_seeker");
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

                    b.Property<Guid?>("AdminProfileId")
                        .HasColumnType("uuid");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint")
                        .HasColumnName("chat_id");

                    b.Property<int>("MessageId")
                        .HasColumnType("integer")
                        .HasColumnName("message_id");

                    b.Property<Guid?>("OfferId")
                        .HasColumnType("uuid")
                        .HasColumnName("offer_id");

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

                    b.HasIndex("AdminProfileId");

                    b.HasIndex("OfferId");

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

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.ActivityType", b =>
                {
                    b.HasOne("ActivitySeeker.Domain.Entities.ActivityType", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.User", b =>
                {
                    b.HasOne("ActivitySeeker.Domain.Entities.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId");

                    b.HasOne("ActivitySeeker.Domain.Entities.Admin", "AdminProfile")
                        .WithMany()
                        .HasForeignKey("AdminProfileId");

                    b.HasOne("ActivitySeeker.Domain.Entities.Activity", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId");

                    b.Navigation("ActivityType");

                    b.Navigation("AdminProfile");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("ActivitySeeker.Domain.Entities.ActivityType", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
