namespace Swipes.Bll.Models;

public record TaskFilter(int Limit, int Offset) : PaginationBase(Limit, Offset);