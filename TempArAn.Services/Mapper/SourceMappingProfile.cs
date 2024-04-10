using AutoMapper;
using TempArAn.Domain.Models.Source;
using TempArAn.Domain.Responses;

namespace TempArAn.Services.Mapper
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<HTMLSource, SourceResponse>()
                .ForMember(r => r.Type, s => s.MapFrom(x => nameof(x)))
                .ForMember(r => r.TypeServiceInfo, s => s.MapFrom(x =>
                new Dictionary<string, string>()
                {
                    { "url", x.Url.ToString() },
                    { "left", x.Left },
                    { "right", x.Right },
                }));
        }
    }
}
