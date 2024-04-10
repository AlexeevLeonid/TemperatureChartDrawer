using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Requests
{
    public class GetRecordsFromSourceQuery : RequestId<List<SimpleRecordResponse>>
    {
        public GetRecordsFromSourceQuery(Guid guid, IUser user) : base(guid, user) { }
    }
}
