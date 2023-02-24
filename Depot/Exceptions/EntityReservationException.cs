namespace Depot.Exceptions;

public class EntityReservationException : Exception
{
    public EntityReservationException() : base()
    {
    }

    public EntityReservationException(string message) : base(message)
    {
    }

    public EntityReservationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}