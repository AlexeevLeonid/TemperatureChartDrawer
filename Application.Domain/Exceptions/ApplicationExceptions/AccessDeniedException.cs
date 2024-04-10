namespace TempArAn.Domain.Exceptions.ApplicationExceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message) { }
    }
}
