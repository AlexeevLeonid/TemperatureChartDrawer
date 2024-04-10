using TempAnAr;

internal class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureAppConfiguration(builder =>
            {
                builder
                    .AddEnvironmentVariables()
                    .AddUserSecrets("5004415f-79e4-41ec-92e7-3779e8b8d068");
            });
}


//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidIssuer = "your-issuer",
//            ValidateAudience = true,
//            ValidAudience = "your-audience",
//            ValidateLifetime = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret")),
//            ValidateIssuerSigningKey = true
//        };
//    });

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

//string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

//builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddTransient<IRecordRepository, SRecordRepository>();
//builder.Services.AddTransient<ISourceRepository, HTMLSourceRepository>();

//builder.Services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();

//builder.Services.AddHostedService<RecorderHostedService>();

//builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddAutoMapper(typeof(UserMappingProfile), typeof(RecordMappingProfile));




//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); ;
//                      });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseDefaultFiles();
//app.UseStaticFiles();

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.UseCors(MyAllowSpecificOrigins);

//app.MapGet("/data", async context => {
//    context.Response.ContentType = "text/html; charset=utf-8";
//    await context.Response.SendFileAsync("wwwroot\\data.html");
//});

//app.MapGet("/create", async context => {
//    context.Response.ContentType = "text/html; charset=utf-8";
//    await context.Response.SendFileAsync("wwwroot\\create.html");
//});

//app.Run();
