using AutoMapper;
using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Source.Handlers
{
    public class GetSourcesQueryHandler : IRequestHandler<GetSourcesQuery, SourcesSetResponse>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSourcesQueryHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SourcesSetResponse> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
        {
            var sourcesSet = await _unitOfWork.Sources.GetSoursesAsync(request.User);
            var result = new SourcesSetResponse(
                _mapper.Map<List<SourceResponse>>(sourcesSet.userList),
                _mapper.Map<List<SourceResponse>>(sourcesSet.publicList));
            return result;
        }
    }
}
