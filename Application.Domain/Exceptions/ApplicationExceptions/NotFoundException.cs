namespace TempArAn.Domain.Exceptions.ApplicationExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
