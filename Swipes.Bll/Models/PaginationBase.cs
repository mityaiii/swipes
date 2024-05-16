namespace Swipes.Bll.Models;

public record PaginationBase(int Limit, int Offset)
{
    public int Limit { get; init; } = 10;
    public int Offset { get; init; } = 0;
}