namespace DreamLuso.Application.Common.Exceptions;

// Exceptions are for exceptional cases
public abstract class ApplicationException : Exception
{
    //public ApplicationException() { }

    public ApplicationException(string msg) : base(msg) { }
}
