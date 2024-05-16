namespace Swipes.Dal.Exceptions;

public class NotFoundException(string message) : ArgumentException(message);