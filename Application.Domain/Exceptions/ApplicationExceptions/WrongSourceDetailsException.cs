namespace TempArAn.Domain.Exceptions.ApplicationExceptions
{
    public class WrongSourceDetailsException : Exception
    {
        public WrongSourceDetailsException(string message) : base(message) { }
    }
}
