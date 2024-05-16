namespace Swipes.Bll.Models;

public record VoteFilter(int Limit, int Offset) : PaginationBase(Limit, Offset);