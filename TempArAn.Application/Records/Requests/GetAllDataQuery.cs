using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Requests
{
    public class GetAllDataQuery : RequestId<List<ComplexRecordResponse>>
    {
        public GetAllDataQuery(Guid guid, IUser user) : base(guid, user)
        {
        }
    }
}
