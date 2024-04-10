using MediatR;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Source.Requests
{
    public class GetSourcesQuery : IRequest<SourcesSetResponse>
    {
        public IUser? User { get; set; }
        public GetSourcesQuery(IUser? user)
        {
            User = user;
        }
    }
}
