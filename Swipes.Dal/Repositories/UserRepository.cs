using Dapper;
using Microsoft.Extensions.Options;
using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Repositories;

public class UserRepository(IOptions<DalOptions> dalOptions)
    : PgRepositoryBase(dalOptions.Value), IUserRepository
{
    public async Task<long> AddAsync(UserEntityV1 userEntityV1, CancellationToken token)
    {
        const string sqlRequest = 
            """
            insert into users (name, login, password)
            values (@Name, @Login, @Password)
            returning id
            """;

        await using var connection = await GetConnection();
        var id = await connection.QuerySingleAsync<long>(new CommandDefinition(sqlRequest,
            new
            {
                Name = userEntityV1.Name,
                Login = userEntityV1.Login,
                Password = userEntityV1.Password
            }, cancellationToken: token));

        return id;
    }

    public async Task<UserEntityV1> GetAsync(long id, CancellationToken token)
    {
        const string sqlRequest =
            """
            select u.id
                 , u.name
                 , u.login
                 , u.password
              from users u
             where u.id = @Id
            """;

        await using var connection = await GetConnection();
        var userEntity = await connection.QuerySingleAsync<UserEntityV1>(new CommandDefinition(sqlRequest,
        new {
            Id = id
        }, cancellationToken: token));

        return userEntity;
    }

    public async Task<UserEntityV1[]> GetAsync(UserFilter userFilter, CancellationToken token)
    {
        Console.WriteLine($"limit: {userFilter.Limit} offset: {userFilter.Offset}");
        
        const string sqlRequest =
            """
            select u.id
                 , u.name
                 , u.login
                 , u.password
              from users u
             limit @Limit
            offset @Offset
            """;

        await using var connection = await GetConnection();
        var userEntity = await connection.QueryAsync<UserEntityV1>(new CommandDefinition(sqlRequest,
            new {
                Limit = userFilter.Limit,
                Offset = userFilter.Offset
            }, cancellationToken: token));

        return userEntity.ToArray();
    }

    public async Task RemoveAsync(long id, CancellationToken token)
    {
        const string sqlRequest =
            """
            delete from users
             where id = @Id
            """;

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(new CommandDefinition(sqlRequest,
            new
            {
                Id = id
            }, cancellationToken: token));
    }

    public Task UpdateAsync(long id, UserEntityV1 updatedUserEntityV1, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}