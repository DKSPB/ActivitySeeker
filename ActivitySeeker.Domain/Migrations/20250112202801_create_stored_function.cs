using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivitySeeker.Domain.Migrations
{
    public partial class create_stored_function : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        try
	        {
migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION activity_seeker.get_children_activity_types(
	            parent_type_id uuid)
                RETURNS TABLE(id uuid) 
                LANGUAGE 'plpgsql'
                COST 100
                VOLATILE PARALLEL UNSAFE
                ROWS 1000

            AS $BODY$
            declare
                buffer uuid;
            begin

                return query
                with recursive act as (
	                select act.id from activity_seeker.activity_type as act
		                where act.id = parent_type_id

	                union all

	                select act1.id from activity_seeker.activity_type as act1
		                inner join act on act1.parent_id = act.id
	                )
                select act.id from act;
            end;
            $BODY$;");

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
	        }
	        catch (Exception e)
	        {
		        Console.WriteLine(e);
		        throw;
	        }
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS activity_seeker.get_children_activity_types(uuid);");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS activity_seeker.get_activities(boolean, uuid, timestamp without time zone, timestamp without time zone, boolean, integer);");
        }
    }
}
