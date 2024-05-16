namespace Swipes.Api.Dto;

public record UserDto
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public required string Login { get; init; }
}