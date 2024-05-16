using Swipes.Bll.Entities;
using Swipes.Bll.Entities.Enums;
using static System.Enum;

namespace Swipes.Dal.Models;

public class LobbyModel
{
    public string Id { get; init; }
    public required string LobbyStatus { get; init; }
    public required string[] ConnectionIds { get; init; }
    public required long[] UserIds { get; init; }
    public required long OwnerId { get; init; }
    public required string[] TaskTypes { get; init; }

    public LobbyEntityV1 ToEntity()
    {
        TryParse<LobbyStatus>(LobbyStatus, out var lobbyStatus);
        var connections = ConnectionIds.Zip(UserIds, (connectionId, userId) => 
            new LobbyEntityV1.ConnectionIds { ConnectionId = connectionId, UserId = userId }
        ).ToList();

        
        return new LobbyEntityV1
        {
            Id = Id,
            LobbyStatus = lobbyStatus,
            OwnerId = OwnerId,
            Connections = connections
        };
    }
}