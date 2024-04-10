using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TempArAn.Services.Services.UserService;

namespace TempAnAr.Extensions
{
    public static class UserServiceExtension
    {
        public static void AddApplicationUserService(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "your-issuer",
                        ValidateAudience = true,
                        ValidAudience = "your-audience",
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret")),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddScoped<IUserService, UserService>();
        }
    }
}
