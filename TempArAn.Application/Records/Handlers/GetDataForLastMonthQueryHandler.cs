using AutoMapper;
using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Records.Requests;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Responses;
using MathNet.Numerics.LinearRegression;
using TempArAn.Services.Services.ConvertService;

namespace TempArAn.Application.Records.Handlers
{
    public class GetDataForLastMonthQueryHandler
        : IRequestHandler<GetDataForLastMonthQuery, List<ComplexRecordResponse>>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDataForLastMonthQueryHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ComplexRecordResponse>> Handle(
            GetDataForLastMonthQuery request, CancellationToken cancellationToken)
        {
            var dateWindow = 15;
            var sizeWindow = 2;
            var sub = DateTime.Now.AddDays(-dateWindow);
            var sourse = await _unitOfWork.Sources.GetSourceAsync(request.Guid) ??
                throw new NotFoundException("Source not found");
            
            var notConvertedData = new Stack<TemperatureDataSetRecords>();
            RecordConverterHostedService.ConvertingAsync(
                await _unitOfWork.DoubleRecords.GetRecordsFromSourceForTimeAsync(
                request.Guid, DateTime.Now.AddDays(-6).Date, DateTime.Now), 
                notConvertedData);
            var convertedData = notConvertedData.Reverse();

            var actualData = (await _unitOfWork.DataRecords.GetRecordsFromSourceForTimeAsync(
                request.Guid, sub, DateTime.Now.AddDays(-1))).Concat(convertedData);

            var leftWindow = DateTime.Now.DayOfYear - sizeWindow;
            var rightWindow = DateTime.Now.AddDays(dateWindow).DayOfYear + sizeWindow;

            var byWindowData = (await _unitOfWork.DataRecords.GetRecordsFromSourceForTimeAsync(
                request.Guid, DateTime.MinValue, DateTime.Now)). // записи данных за всё врёмя с этого источника
                GroupBy(x => x.DateTime.DayOfYear);
            byWindowData = byWindowData.Where(x =>
                (leftWindow <= x.Key &&
                x.Key <= rightWindow) ||
                (365 - leftWindow <= dateWindow || rightWindow <= dateWindow) &&
                ((0 >= x.Key && x.Key <= rightWindow) || (leftWindow <= x.Key && x.Key <= 365))); // записи за дату в пределах окна
                // группировка записей в окне по дням, в каждом grouping данные по всем годам

            var averageSeries = SmoothDataSeries(byWindowData.Select(x => 
                AverageDataRecordsForThisDay(x, x.First().DateTime)), sizeWindow).
                Reverse();

            var concatSeries = actualData.Concat(averageSeries);

            var firstDay = concatSeries.First().DateTime.DayOfYear;
            var (A, B) = SimpleRegression.Fit(
                concatSeries.Select(x => (double)x.DateTime.DayOfYear).ToArray(),
                concatSeries.Select(x => (double)x.Mean).ToArray());

            var meanTrendLine = concatSeries.Select(x => 
                new TemperatureDataSetRecords(x.SourceId, 
                new DateTime(DateTime.Now.Year, x.DateTime.Month, x.DateTime.Day), 
                x.Min, 
                x.Max, 
                A + (x.DateTime.DayOfYear) * B,  
                x.Mean));

            return _mapper.Map<List<ComplexRecordResponse>>(meanTrendLine);
        }


        public IEnumerable<TemperatureDataSetRecords> SmoothDataSeries(IEnumerable<TemperatureDataSetRecords> series, int sizeWindow)
        {
            if (series == null) return Enumerable.Empty<TemperatureDataSetRecords>();
            var data = series.ToArray();
            var result = new Stack<TemperatureDataSetRecords>();
            var window = new LinkedList<TemperatureDataSetRecords>();
            for (var i = 0; i < sizeWindow * 2; i++) window.AddFirst(data[i]);
            for (var i = sizeWindow * 2 + 1; i < data.Length; i++)
            {
                window.AddFirst(data[i]);
                //result.Push(data[i]);
                result.Push(AverageDataRecordsForThisDay(window, data[i - sizeWindow].DateTime));
                window.RemoveLast();
            }
            return result;
        }

        public TemperatureDataSetRecords AverageDataRecordsForThisDay(
            IEnumerable<TemperatureDataSetRecords> dataForDayRecords, DateTime date) 
            => new(
                dataForDayRecords.First().SourceId,
                date,
                dataForDayRecords.Average(x => x.Min),
                dataForDayRecords.Average(x => x.Max),
                dataForDayRecords.Average(x => x.Mean),
                dataForDayRecords.Average(x => x.Median));
    }
}
