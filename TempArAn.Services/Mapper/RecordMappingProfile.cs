using AutoMapper;
using System.Globalization;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Responses;

namespace TempArAn.Services.Mapper
{
    public class RecordMappingProfile : Profile
    {
        public RecordMappingProfile()
        {
            
            CreateMap<DoubleRecord, SimpleRecordResponse>()
                .ConvertUsing(x => new SimpleRecordResponse(x.Value.ToString(CultureInfo.InvariantCulture), x.DateTime));

            CreateMap<SourceErrorRecord, SimpleRecordResponse>()
                .ConvertUsing(x => new SimpleRecordResponse(x.TypeSourceError.ToString(), x.DateTime));

            CreateMap<TemperatureDataSetRecords, ComplexRecordResponse>()
                .ConstructUsing(x =>
                    new ComplexRecordResponse(
                        new Dictionary<string, string>
                        {
                            { "max" ,       x.Max.ToString(CultureInfo.InvariantCulture) },
                            { "min" ,       x.Min.ToString(CultureInfo.InvariantCulture) },
                            { "mean" ,      x.Mean.ToString(CultureInfo.InvariantCulture) },
                            { "median" ,    x.Median.ToString(CultureInfo.InvariantCulture) }
                        }, x.DateTime)
                    );
        }

    }
}
