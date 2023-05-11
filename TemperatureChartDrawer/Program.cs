using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using TemperatureChartDrawer.src;
using TemperatureChartDrawer.src.Database.Entity;
using TemperatureChartDrawer.src.Database.Interfaces;
using TemperatureChartDrawer.src.Recorder;
using TemperatureChartDrawer.src.Sourse;

var builder = WebApplication.CreateBuilder(args);

//jenerik-controller
builder.Services
    .AddControllers(opt =>
    opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", new MediaTypeHeaderValue("application/xml")))
    .ConfigureApplicationPartManager(manager =>
    {
        manager.FeatureProviders.Add(new GenericFeatureProvider());
    }
    ).AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddSingleton<IRepository<HTMLSource>, EntityRepository>();

builder.Services.AddHostedService<RecorderService<HTMLSource>>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/data", async context => {
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot\\data.html");
});

app.MapGet("/create", async context => {
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot\\create.html");
});

app.Run();
