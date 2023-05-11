using Microsoft.AspNetCore.Mvc;
using TemperatureChartDrawer.src.Database.Interfaces;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.Controllers
{
    [ApiController]
    [Route("source")]
    public class SourceController<TSource> : ControllerBase
        where TSource : SourceBase
    {
        private readonly ILogger<SourceController<TSource>> _logger;
        private readonly IRepository<TSource> _rep;

        public SourceController(ILogger<SourceController<TSource>> logger, IRepository<TSource> rep)
        {
            _logger = logger;
            _rep = rep;
        }

        [HttpGet("{guid}")]
        public async Task<TSource> GetSource(Guid guid)
        {
            return await _rep.GetSourse(guid);
        }

        [HttpGet("")]
        public async Task<List<TSource>> GetSources()
        {
            return await _rep.GetSourses();
        }

        [HttpPost("")]
        public async Task PostSource([FromBody] TSource source)
        {
            await _rep.PostSourse(source);
            _logger.Log(LogLevel.Information, $"post source {source.Id}");
        }

        [HttpDelete("{id}")]
        public async Task DeleteSource(Guid id)
        {
            await _rep.DeleteSourse(id);
            _logger.Log(LogLevel.Information, $"delete source {id}");
        }
    }
}
