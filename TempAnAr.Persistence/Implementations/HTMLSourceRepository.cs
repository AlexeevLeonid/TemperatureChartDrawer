using AutoMapper;
using TempAnAr.Persistence.Base;
using TempAnAr.Persistence.Context;
using TempArAn.Domain.Models.Source;

namespace TempAnAr.Persistence.Implementations
{
    public class HTMLSourceRepository : SourceRepository<HTMLSource>
    {
        public HTMLSourceRepository(ApplicationContext context) : base(context, context.Sources) { }
    }
}
