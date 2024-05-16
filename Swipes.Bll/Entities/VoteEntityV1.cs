using Swipes.Bll.Entities.Enums;

namespace Swipes.Bll.Entities;

public record VoteEntityV1
{
    public Guid Id { get; init; }
    public required long TaskId { get; init; }
    public required Opinion Opinion { get; init; }
    public required long UserId { get; init; }
    public required string LobbyId { get; init; }
}