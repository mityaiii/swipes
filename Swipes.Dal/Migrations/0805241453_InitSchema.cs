using FluentMigrator;

namespace Swipes.Dal.Migrations;

[Migration(0805241453, TransactionBehavior.None)]
public class InitSchema : Migration {
    private const int SmallStringLength = 55;
    
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey("users_pk").Identity()
            .WithColumn("name").AsString(SmallStringLength).NotNullable()
            .WithColumn("login").AsString(SmallStringLength).NotNullable()
            .WithColumn("password").AsString(SmallStringLength).NotNullable();

        Create.Table("map_points")
            .WithColumn("id").AsInt64().PrimaryKey("map_points_pk").Identity()
            .WithColumn("longitude").AsDecimal(7, 3)
            .WithColumn("lantitude").AsDecimal(7, 3)
            .WithColumn("name").AsString(SmallStringLength);

        Create.Table("tasks")
            .WithColumn("id").AsInt64().PrimaryKey("tasks_pk").Identity()
            .WithColumn("name").AsString(SmallStringLength).NotNullable()
            .WithColumn("type").AsString(SmallStringLength).NotNullable()
            .WithColumn("map_point_id").AsInt64().ForeignKey("map_points", "id")
            .WithColumn("description").AsString();
        
        Create.Table("events")
            .WithColumn("id").AsInt64().PrimaryKey("events_pk").Identity()
            .WithColumn("name").AsString(SmallStringLength).NotNullable()
            .WithColumn("type").AsString(SmallStringLength).NotNullable()
            .WithColumn("map_point_id").AsInt64().ForeignKey("map_points", "id")
            .WithColumn("description").AsString();

        Create.Table("lobbies")
            .WithColumn("id").AsString(16).PrimaryKey("lobby_pk")
            .WithColumn("lobby_status").AsString(10).NotNullable()
            .WithColumn("task_types").AsCustom("VARCHAR(10)[]")
            .WithColumn("owner_id").AsInt64().ForeignKey("users", "id").NotNullable()
            .WithColumn("connection_ids").AsCustom("VARCHAR(50)[]")
            .WithColumn("user_ids").AsCustom("BIGINT[]");
        
        Create.Table("votes")
            .WithColumn("task_id").AsInt64().NotNullable().PrimaryKey()
            .WithColumn("user_id").AsInt64().NotNullable().PrimaryKey()
            .WithColumn("lobby_id").AsString(16).NotNullable().PrimaryKey()
            .WithColumn("opinion").AsString(8);

        Create.ForeignKey()
            .FromTable("votes").ForeignColumn("lobby_id")
            .ToTable("lobbies").PrimaryColumn("id");
        
        Create.ForeignKey()
            .FromTable("votes").ForeignColumn("task_id")
            .ToTable("tasks").PrimaryColumn("id");

        Create.ForeignKey()
            .FromTable("votes").ForeignColumn("user_id")
            .ToTable("users").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("tasks");
        Delete.Table("events");
        Delete.Table("map_points");
    }
}