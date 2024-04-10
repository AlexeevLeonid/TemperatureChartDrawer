using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Records.Requests;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Records.Handlers
{
    internal class GetAllDataQueryHandler : IRequestHandler<GetAllDataQuery, List<ComplexRecordResponse>>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllDataQueryHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ComplexRecordResponse>> Handle(
            GetAllDataQuery request, CancellationToken cancellationToken)
        {
            var sourse = await _unitOfWork.Sources.GetSourceAsync(request.Guid) ??
                throw new NotFoundException("Source not found");
            var rawData = await _unitOfWork.DataRecords.GetRecordsFromSourseAsync(request.Guid);
            var result = new List<TemperatureDataSetRecords>();
            foreach (var dataByMonth in rawData.OrderBy(x => x.DateTime.Month).GroupBy(x => x.DateTime.Month)) 
            {

                result.Add(new TemperatureDataSetRecords(
                    dataByMonth.First().SourceId,
                    new DateTime(DateTime.Now.AddYears(-1).Year, dataByMonth.First().DateTime.Month, 1),
                    dataByMonth.Min(x => x.Min),
                    dataByMonth.Max(x => x.Max),
                    dataByMonth.Average(x => x.Mean),
                    dataByMonth.Average(x => x.Median)
                    ));
            }
            return _mapper.Map<List<ComplexRecordResponse>>(result);
        }
    }
}
