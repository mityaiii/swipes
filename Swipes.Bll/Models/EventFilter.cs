namespace Swipes.Bll.Models;

public record EventFilter(int Limit, int Offset) : PaginationBase(Limit, Offset);