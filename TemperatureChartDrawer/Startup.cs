using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TempAnAr.Extensions;
using TempArAn.Application.User.Requests;
using TempArAn.Domain.Validators;

namespace TempAnAr
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        private IConfiguration Configuration { get; }

        private IHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(UserValidator));
            services.AddMediatR(typeof(LoginCommand));
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApplicationDb(Configuration.GetConnectionString("DefaultConnection"));
            services.AddApplicationRecorderService();
            services.AddApplicationUserService();
            services.AddHttpContextAccessor();
            services.AddApplicationMapper();
            services.AddApplicationConverterService();
            //var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApplicationExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(); //"_myAllowSpecificOrigins"
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
