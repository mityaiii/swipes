using System.Security.Cryptography;
using Dapper;
using Microsoft.Extensions.Options;
using Swipes.Bll.Entities;
using Swipes.Bll.Repositories;
using Swipes.Dal.Extensions;
using Swipes.Dal.Models;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Repositories;

public class LobbyRepository(IOptions<DalOptions> dalOptions) : 
    PgRepositoryBase(dalOptions.Value), ILobbyRepository
{
    public async Task<LobbyEntityV1?> GetUserLobbyAsync(long userId)
    {
        const string sqlRequest =
            """
            select *
              from lobbies l
             where @UserId = any(l.user_ids);
            """;

        await using var connection = await GetConnection();
        var lobby = await connection.QuerySingleAsync<LobbyModel>(new CommandDefinition(sqlRequest, new
        {
            UserId = userId   
        }));

        return lobby.ToEntity();
    }

    public async Task<string> AddAsync(LobbyEntityV1 lobbyEntityV1)
    {
        var lobbyModel = lobbyEntityV1.ToModel();
        
        const string sqlRequest = 
            """
            insert into lobbies (id, lobby_status, owner_id, task_types, connection_ids, user_ids)
            values (@Id, @LobbyStatus, @OwnerId, @TaskTypes, @ConnectionIds, @UserIds);
            """;
    
        await using var connection = await GetConnection();
        var id = await connection.QuerySingleAsync<string>(new CommandDefinition(sqlRequest, new
        {
            Id = ShortenGuid(Guid.NewGuid()),
            LobbyStatus = lobbyModel.LobbyStatus,
            OwnerId = lobbyModel.OwnerId,
            TaskTypes = lobbyModel.TaskTypes,
            ConnectionIds = lobbyModel.ConnectionIds,
            UserIds = lobbyModel.UserIds
        }));

        return id;
    }

    public async Task<LobbyEntityV1?> GetAsync(string id)
    {
        const string sqlRequest = 
            """
            select id
                 , lobby_status
                 , owner_id
                 , task_types
                 , connection_ids
                 , user_ids
            from lobbies
            where id = @Id;
            """;
    
        await using var connection = await GetConnection();
        var result = await connection.QuerySingleAsync<LobbyModel>(new CommandDefinition(sqlRequest, new
        {
            Id = id
        }));
        
        return result.ToEntity();
    }

    public async Task RemoveAsync(string id)
    {
        const string sqlRequest =
            """
            delete from lobbies
             where id = @Id
            """;

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(new CommandDefinition(sqlRequest, new
            {
                Id = id
            }));
    }

    public async Task UpdateAsync(LobbyEntityV1 updatedLobbyEntityV1)
    {
        var lobbyModel = updatedLobbyEntityV1.ToModel();            

        const string sqlRequest = 
            """
            update lobbies
               set lobby_status = @LobbyStatus
                 , connection_ids = @ConnectionIds
                 , user_ids = @UserIds
                 , task_types = @TaskTypes
            """;

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(new CommandDefinition(sqlRequest, new
        {
            LobbyStatus = lobbyModel.LobbyStatus,
            ConnectionIds = lobbyModel.ConnectionIds,
            UserIds = lobbyModel.UserIds,
            TaskTypes = lobbyModel.TaskTypes
        }));
    }
    
    private string ShortenGuid(Guid guid)
    {
        var hash = MD5.HashData(guid.ToByteArray());
        return BitConverter.ToString(hash).Replace("-", "")[..16];
    }
}
