namespace Swipes.Bll.Models;

public record UserFilter(int Limit, int Offset) : PaginationBase(Limit, Offset);