using MediatR;
using TempArAn.Domain.AbstractCore;

namespace TempArAn.Application.Base
{
    public abstract class RequestId<TEntity> : IRequest<TEntity>
    {
        public Guid Guid { get; set; }
        public IUser User { get; set; }

        protected RequestId(Guid guid, IUser user)
        {
            Guid = guid;
            User = user;
        }
    }
}
