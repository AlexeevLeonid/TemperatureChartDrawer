using TempArAn.Domain.Enums;
using TempArAn.Domain.Exceptions.SourceExeptions;
using TempArAn.Domain.Exceptions.SourceExeptions.HtmlSourceExceptions;

namespace TempArAn.Domain.Models.Record
{
    public class SourceErrorRecord : SimpleRecordBase<int>
    {
        public TypeSourceError TypeSourceError { get => (TypeSourceError)Value; }
        public SourceErrorRecord(Guid sourceId, DateTime dateTime, int value) :
            base(sourceId, dateTime, value)
        {
        }
        public SourceErrorRecord(Guid sourceId, DateTime dateTime, TypeSourceError error) :
            base(sourceId, dateTime, (int)error)
        {
        }

        public SourceErrorRecord(Guid sourceId, DateTime dateTime, SourceException sourceException) :
            base(sourceId, dateTime, GetSourceExceptionCode(sourceException))
        {
        }

        private static int GetSourceExceptionCode(SourceException sourceException)
        {
            switch (sourceException)
            {
                case NotFoundHtmlSourceException: return 0;
                case ParseErrorHtmlSourceException: return 1;
            }
            throw new ArgumentException("There is not a SourceException");
        }
    }
}
