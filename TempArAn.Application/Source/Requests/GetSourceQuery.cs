using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Source.Requests
{
    public class GetSourceQuery : RequestId<SourceResponse>
    {
        public GetSourceQuery(Guid guid, IUser user) : base(guid, user) { }
    }
}
