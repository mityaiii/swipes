using Swipes.Bll.Entities.Enums;

namespace Swipes.Bll.Entities;

public record LobbyEntityV1
{
    public string Id { get; init; }
    public required LobbyStatus LobbyStatus { get; init; }
    public required List<ConnectionIds> Connections { get; init; }

    public required long OwnerId { get; init; }

    public List<TaskType> TaskTypes { get; init; } = new List<TaskType>();

    public class ConnectionIds
    {
        public required long UserId { get; init; }
        public required string ConnectionId { get; set; }
    }
}
