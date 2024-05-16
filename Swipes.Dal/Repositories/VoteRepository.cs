using Dapper;
using Microsoft.Extensions.Options;
using Swipes.Bll.Entities;
using Swipes.Bll.Entities.Enums;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Repositories;

public class VoteRepository(IOptions<DalOptions> dalOptions)
    : PgRepositoryBase(dalOptions.Value), IVoteRepository
{
    public async Task AddAsync(VoteEntityV1 voteEntityV1)
    {   
        const string sqlRequest = 
            """
            insert into votes (task_id, opinion, user_id, lobby_id)
            values (@TaskId, @Opinion, @UserId, @LobbyId)
                on conflict(task_id, user_id, lobby_id)
            do update set opinion = excluded.opinion;
            """;

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(new CommandDefinition(sqlRequest, new
        {
            TaskId = voteEntityV1.TaskId,
            Opinion = voteEntityV1.Opinion,
            LobbyId = voteEntityV1.LobbyId,
            UserId = voteEntityV1.UserId
        }));
    }

    public Task UpdateAsync(long taskId, long userId, long lobbyId, Opinion newOpinion)
    {
        throw new NotImplementedException();
    }

    public Task<VoteEntityV1> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<VoteEntityV1[]> GetLobbyVotesAboutTask(long taskId, string lobbyId)
    {
        const string sqlRequest =
            """
            select *
             from votes v 
            where v.lobby_id = @LobbyId
              and v.task_id = @TaskId;
            """;

        await using var connection = await GetConnection();
        var votes = await connection.QueryAsync<VoteEntityV1>(new CommandDefinition(sqlRequest, new
        {
            LobbyId = lobbyId,
            TaskId = taskId
        }));

        return votes.ToArray();
    }

    public Task<VoteEntityV1[]> GetAsync(VoteFilter voteFilter)
    {
        throw new NotImplementedException();
    }
}