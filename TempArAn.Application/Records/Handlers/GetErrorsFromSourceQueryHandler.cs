using AutoMapper;
using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Records.Requests;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Handlers
{
    public class GetErrorsFromSourceQueryHandler
        : IRequestHandler<GetErrorsFromSourceQuery, List<SimpleRecordResponse>>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetErrorsFromSourceQueryHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<SimpleRecordResponse>> Handle(
            GetErrorsFromSourceQuery request, CancellationToken cancellationToken)
        {
            var sourse = await _unitOfWork.Sources.GetSourceAsync(request.Guid) ??
                throw new NotFoundException("Source not found");
            var result = await _unitOfWork.ErrorRecords.GetRecordsFromSourseAsync(request.Guid);
            return _mapper.Map<List<SimpleRecordResponse>>(result);
        }
    }
}
