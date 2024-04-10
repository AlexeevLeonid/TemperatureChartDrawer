using TempArAn.Application.Base;
using TempArAn.Domain.AbstractCore;

namespace TempArAn.Application.Source.Requests
{
    internal class SetRecordingStateCommand : RequestId<bool>
    {
        public bool Value { get; set; }
        public SetRecordingStateCommand(Guid guid, IUser user, bool value) : base(guid, user)
        {
        }
    }
}
