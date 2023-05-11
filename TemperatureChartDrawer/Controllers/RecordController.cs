using Microsoft.AspNetCore.Mvc;
using TemperatureChartDrawer.src.Database.Interfaces;
using TemperatureChartDrawer.src.Recorder;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.Controllers
{
    [ApiController]
    [Route("record")]
    public class RecordController<TSource>: ControllerBase
        where TSource : SourceBase
    {

        private readonly ILogger<RecordController<TSource>> _logger;
        private readonly IRepository<TSource> _rep;

        public RecordController(IRepository<TSource> rep, ILogger<RecordController<TSource>> logger)
        {
            _logger = logger;
            _rep = rep;
        }

        [HttpGet("{id}")]
        public async Task<List<ShortRecord>> GetRecords(Guid id)
        {
            return await _rep.GetRecordsFromSourse(id);
        }
    }
}