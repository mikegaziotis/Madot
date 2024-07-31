namespace Madot.Core.Application.Exceptions;

public class EntityConflictException(string message, Exception? innerException = null) : Exception(message, innerException);
public class EntityNotFoundException(string message, Exception? innerException = null) : Exception(message, innerException);

public class InvalidArgumentException(string message, Exception? innerException = null) : Exception(message, innerException);

public class UnexpectedDatabaseResultException(string message, Exception? innerException = null) : Exception(message, innerException);
public class DatabaseException(string message, Exception? innerException = null) : Exception(message, innerException);

public class UnknownApplicationException(string message, Exception? innerException = null) : Exception(message, innerException);