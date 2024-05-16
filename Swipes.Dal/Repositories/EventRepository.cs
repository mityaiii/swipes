using Dapper;
using Microsoft.Extensions.Options;
using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Repositories;

public class EventRepository(IOptions<DalOptions> dalOptions) 
    : PgRepositoryBase(dalOptions.Value), IEventRepository
{
    public async Task<long> AddAsync(EventEntityV1 eventEntityV1, CancellationToken token)
    {
        const string sqlRequest = 
            """
            insert into events (name, type, map_point_id, description, start_at, end_at)
            values (@Name, @Type, @MapPointId, @Description, @StartAt, @EndAt)
            returning id
            """;

        await using var connection = await GetConnection();
        var id = await connection.QuerySingleAsync<long>(new CommandDefinition(sqlRequest,
            new
            {
                Name = eventEntityV1.Name,
                Type = eventEntityV1.Type,
                MapPointId = eventEntityV1.MapPointId,
                Desciption = eventEntityV1.Description,
                taskEntityV1 = eventEntityV1.StartAt,
                StartAt = eventEntityV1.EndAt,
            }, cancellationToken: token));

        return id;
    }

    public async Task<EventEntityV1> GetAsync(long id, CancellationToken token)
    {
        const string sqlRequest =
            """
            select *
              from events e
             where e.id = @Id
            """;

        await using var connection = await GetConnection();
        var eventEntity = await connection.QuerySingleAsync<EventEntityV1>(new CommandDefinition(sqlRequest,
        new {
            Id = id
        }, cancellationToken: token));

        return eventEntity;
    }

    public async Task<EventEntityV1[]> GetAsync(EventFilter eventFilter, CancellationToken token)
    {
        const string sqlRequest =
            """
            select *
              from events
             limit @Limit
            offset @Offset
            """;

        await using var connection = await GetConnection();
        var eventEntity = await connection.QueryAsync<EventEntityV1>(new CommandDefinition(sqlRequest,
            new {
                Limit = eventFilter.Limit,
                Offset = eventFilter.Offset
            }, cancellationToken: token));

        return eventEntity.ToArray();
    }

    public async Task RemoveAsync(long id, CancellationToken token)
    {
        const string sqlRequest =
            """
            delete from events
             where id = @Id
            """;

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(new CommandDefinition(sqlRequest,
            new
            {
                Id = id
            }, cancellationToken: token));
    }

    public Task UpdateAsync(long id, EventEntityV1 updatedEventEntityV1, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}