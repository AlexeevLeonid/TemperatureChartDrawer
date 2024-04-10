using AutoMapper;
using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Source.Handlers
{
    public class GetSourceQueryHandler : IRequestHandler<GetSourceQuery, SourceResponse>
    {

        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSourceQueryHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SourceResponse> Handle(GetSourceQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Sources.GetSourceAsync(request.Guid);
            return _mapper.Map<SourceResponse>(result);
        }
    }
}
