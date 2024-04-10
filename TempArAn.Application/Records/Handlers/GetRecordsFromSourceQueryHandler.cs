using AutoMapper;
using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Records.Requests;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Handlers
{
    public class GetRecordsFromSourceQueryHandler
        : IRequestHandler<GetRecordsFromSourceQuery, List<SimpleRecordResponse>>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRecordsFromSourceQueryHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<SimpleRecordResponse>> Handle(
            GetRecordsFromSourceQuery request, CancellationToken cancellationToken)
        {
            var sourse = await _unitOfWork.Sources.GetSourceAsync(request.Guid) ??
                throw new NotFoundException("Source not found");
            var result = await _unitOfWork.DoubleRecords.GetRecordsFromSourceForTimeAsync(
                request.Guid, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now, false);
            return _mapper.Map<List<SimpleRecordResponse>>(result);
        }
    }
}
