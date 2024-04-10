using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Requests
{
    public class GetDataForLastMonthQuery : RequestId<List<ComplexRecordResponse>>
    {
        public GetDataForLastMonthQuery(Guid guid, IUser user) : base(guid, user)
        {
        }
    }
}
