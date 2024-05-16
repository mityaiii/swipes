using Swipes.Bll.Entities;
using Swipes.Dal.Models;

namespace Swipes.Dal.Extensions;

public static class LobbyEntity2LobbyModel
{
    public static LobbyModel ToModel(this LobbyEntityV1 lobbyEntityV1)
    {
        return new LobbyModel
        {
            Id = lobbyEntityV1.Id,
            LobbyStatus = lobbyEntityV1.LobbyStatus.ToString(),
            OwnerId = lobbyEntityV1.OwnerId,
            ConnectionIds = lobbyEntityV1.Connections.Select(c => c.ConnectionId).ToArray(),
            UserIds = lobbyEntityV1.Connections.Select(c => c.UserId).ToArray(),
            TaskTypes = lobbyEntityV1.TaskTypes.Select(t => t.ToString()).ToArray()
        };
    }
}