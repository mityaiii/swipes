using Swipes.Bll.Entities;
using Swipes.Bll.Entities.Enums;
using Swipes.Bll.Models;

namespace Swipes.Bll.Repositories;

public interface IVoteRepository
{
    Task AddAsync(VoteEntityV1 voteEntityV1);
    Task UpdateAsync(long taskId, long userId, long lobbyId, Opinion newOpinion);
    Task<VoteEntityV1> GetAsync(long id);

    Task<VoteEntityV1[]> GetLobbyVotesAboutTask(long taskId, string lobbyId);
    Task<VoteEntityV1[]> GetAsync(VoteFilter voteFilter);
}