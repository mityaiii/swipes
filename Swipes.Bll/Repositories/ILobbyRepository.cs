using Swipes.Bll.Entities;

namespace Swipes.Bll.Repositories;

public interface ILobbyRepository
{
    Task<LobbyEntityV1?> GetUserLobbyAsync(long userId);
    Task<string> AddAsync(LobbyEntityV1 lobbyEntityV1);
    Task<LobbyEntityV1?> GetAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(LobbyEntityV1 updatedLobbyEntityV1);
}