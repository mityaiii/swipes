using Dapper;
using Microsoft.Extensions.Options;
using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Repositories;

public class TaskRepository(IOptions<DalOptions> dalOptions) 
    : PgRepositoryBase(dalOptions.Value), ITaskRepository
{
    public async Task<long> AddAsync(TaskEntityV1 taskEntityV1, CancellationToken token)
    {
        const string sqlRequest = 
            """
            insert into tasks (name, type, map_point_id, description)
            values (@Name, @Type, @MapPointId, @Description)
            returning id
            """;

        await using var connection = await GetConnection();
        var id = await connection.QuerySingleAsync<long>(new CommandDefinition(sqlRequest,
            new
            {
                Name = taskEntityV1.Name,
                Type = taskEntityV1.Type,
                MapPointId = taskEntityV1.MapPointId,
                Desciption = taskEntityV1.Description,
            }, cancellationToken: token));

        return id;
    }

    public async Task<TaskEntityV1> GetAsync(long id, CancellationToken token)
    {
        const string sqlRequest =
            """
            select *
              from tasks t
             where t.id = @Id
            """;

        await using var connection = await GetConnection();
        var taskEntity = await connection.QuerySingleAsync<TaskEntityV1>(new CommandDefinition(sqlRequest,
        new {
            Id = id
        }, cancellationToken: token));

        return taskEntity;
    }

    public async Task<TaskEntityV1[]> GetAsync(TaskFilter taskFilter, CancellationToken token)
    {
        const string sqlRequest =
            """
            select *
              from tasks
             limit @Limit
            offset @Offset
            """;

        await using var connection = await GetConnection();
        var taskEntity = await connection.QueryAsync<TaskEntityV1>(new CommandDefinition(sqlRequest,
            new {
                Limit = taskFilter.Limit,
                Offset = taskFilter.Offset
            }, cancellationToken: token));

        return taskEntity.ToArray();
    }

    public async Task RemoveAsync(long id, CancellationToken token)
    {
        const string sqlRequest =
            """
            delete from tasks
             where id = @Id
            """;

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(new CommandDefinition(sqlRequest,
            new
            {
                Id = id
            }, cancellationToken: token));
    }

    public Task UpdateAsync(long id, TaskEntityV1 updatedTaskEntityV1, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}