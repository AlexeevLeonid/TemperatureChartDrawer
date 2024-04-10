using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;

namespace TempArAn.Application.Source.Requests
{
    public class DeleteSourceCommand : RequestId<bool>
    {
        public DeleteSourceCommand(Guid guid, IUser user) : base(guid, user) { }
    }
}
