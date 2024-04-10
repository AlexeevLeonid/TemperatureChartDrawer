using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Requests
{
    public class GetErrorsFromSourceQuery : RequestId<List<SimpleRecordResponse>>
    {
        public GetErrorsFromSourceQuery(Guid guid, IUser user) : base(guid, user)
        {
        }
    }
}
