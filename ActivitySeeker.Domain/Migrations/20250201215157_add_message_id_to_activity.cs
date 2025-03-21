﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class add_message_id_to_activity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS activity_seeker.get_activities(boolean, uuid, timestamp without time zone, timestamp without time zone, boolean, integer);");

            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION activity_seeker.get_activities(
	            online boolean,
	            type_id uuid,
	            search_from timestamp without time zone,
	            search_to timestamp without time zone,
	            published boolean,
	            city_guid integer)
                RETURNS TABLE(id uuid, link_description text, image bytea, start_date timestamp without time zone, activity_type_id uuid, is_published boolean, city_id integer, is_online boolean, tg_message_id integer) 
                LANGUAGE 'plpgsql'
                COST 100
                VOLATILE PARALLEL UNSAFE
                ROWS 1000

                AS $BODY$

                    declare
                    type_guid uuid;
                    begin
   
                    if (type_id is NULL) then
                    return query 
	                    select ac.id, ac.link_description, NULL::bytea, ac.start_date, ac.activity_type_id, ac.is_published, ac.city_id, ac.is_online, ac.tg_message_id
		                    from activity_seeker.activity as ac
		                    where
		                    (search_from is NULL or ac.start_date >= search_from) and
		                    (search_to is NULL or ac.start_date <= search_to) and
		                    (published is NULL or ac.is_published = published) and
		                    (city_guid is NULL or ac.city_id = city_guid) and
		                    (online is NULL or ac.is_online = online );
                    else
      
                    for type_guid in select g.id from activity_seeker.get_children_activity_types(type_id) as g
                    loop
	                    return query
		                    select ac.id, ac.link_description, NULL::bytea, ac.start_date, ac.activity_type_id, ac.is_published, ac.city_id, ac.is_online, ac.tg_message_id 
			                    from activity_seeker.activity as ac 
			                    where 
			                    (ac.activity_type_id = type_guid) and
			                    (search_from is NULL or ac.start_date >= search_from) and
			                    (search_to is NULL or ac.start_date <= search_to) and
			                    (published is NULL or ac.is_published = published) and
			                    (city_guid is NULL or ac.city_id = city_guid) and
			                    (online is NULL or ac.is_online = online );
                    end loop;
                    end if;
                    end;
            
            $BODY$;");

            migrationBuilder.AddColumn<int>(
                name: "tg_message_id",
                schema: "activity_seeker",
                table: "activity",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2025, 2, 4, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(273));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2025, 2, 5, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(259));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2025, 2, 2, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(269));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2025, 2, 10, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(243));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2025, 2, 3, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2025, 3, 2, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(265));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2025, 2, 7, 0, 51, 56, 845, DateTimeKind.Local).AddTicks(277));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS activity_seeker.get_activities(boolean, uuid, timestamp without time zone, timestamp without time zone, boolean, integer);");

            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION activity_seeker.get_activities(
	            online boolean,
	            type_id uuid,
	            search_from timestamp without time zone,
	            search_to timestamp without time zone,
	            published boolean,
	            city_guid integer)
                RETURNS TABLE(id uuid, link_description text, image bytea, start_date timestamp without time zone, activity_type_id uuid, is_published boolean, city_id integer, is_online boolean) 
                LANGUAGE 'plpgsql'
                COST 100
                VOLATILE PARALLEL UNSAFE
                ROWS 1000

            AS $BODY$
               declare
                  type_guid record;
               begin
   
                  if (type_id is NULL) then
	                 return query 
			             select ac.id, ac.link_description, NULL::bytea, ac.start_date, ac.activity_type_id, ac.is_published, ac.city_id, ac.is_online
				             from activity_seeker.activity as ac
				             where
				             (search_from is NULL or ac.start_date >= search_from) and
				             (search_to is NULL or ac.start_date <= search_to) and
				             (published is NULL or ac.is_published = published) and
				             (city_guid is NULL or ac.city_id = city_guid) and
				             (online is NULL or ac.is_online = online );
	              else
      
	                  for type_guid in select g.id from activity_seeker.get_children_activity_types(type_id) as g
		              loop
		                  return query
			                  select ac.id, ac.link_description, NULL::bytea, ac.start_date, ac.activity_type_id, ac.is_published, ac.city_id, ac.is_online 
				                  from activity_seeker.activity as ac 
					              where 
					              (ac.activity_type_id = type_guid.id) and
					              (search_from is NULL or ac.start_date >= search_from) and
					              (search_to is NULL or ac.start_date <= search_to) and
					              (published is NULL or ac.is_published = published) and
					              (city_guid is NULL or ac.city_id = city_guid) and
					              (online is NULL or ac.is_online = online );
		              end loop;
	              end if;
               end;
   
            $BODY$;");

            migrationBuilder.DropColumn(
                name: "tg_message_id",
                schema: "activity_seeker",
                table: "activity");

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("2b7c542f-8070-49b3-a20d-e2864b0b8383"),
                column: "start_date",
                value: new DateTime(2025, 2, 3, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2244));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("43955f98-0bdc-4ca6-ad25-604e186e3751"),
                column: "start_date",
                value: new DateTime(2025, 2, 4, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2228));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("4564a97f-fe6a-4493-9adc-7a5278b59937"),
                column: "start_date",
                value: new DateTime(2025, 2, 1, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2239));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                column: "start_date",
                value: new DateTime(2025, 2, 9, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2213));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("86c75c6b-43aa-42b6-8154-a6306f2c1cc7"),
                column: "start_date",
                value: new DateTime(2025, 2, 2, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2231));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("88ce103e-3f4a-4829-92a4-8d318447f3e6"),
                column: "start_date",
                value: new DateTime(2025, 3, 1, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2235));

            migrationBuilder.UpdateData(
                schema: "activity_seeker",
                table: "activity",
                keyColumn: "id",
                keyValue: new Guid("c4513d82-5a21-4583-bac8-71b869c8c57c"),
                column: "start_date",
                value: new DateTime(2025, 2, 6, 19, 19, 55, 386, DateTimeKind.Local).AddTicks(2247));
        }
    }
}
