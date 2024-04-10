using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Models.User;
using TempArAn.Domain.Requests;

namespace TempArAn.Services.Services.UserService
{

    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _rep;
        private readonly IValidator<IUser> _validator;
        public UserService(IApplicationUnitOfWork rep, IValidator<IUser> validator)
        {
            _rep = rep;
            _validator = validator;
        }

        public async Task<IUser> GetUserByIdAsync(Guid id)
        {
            return await _rep.Users.GetUserAsync(id) ??
                throw new AccessDeniedException("user not found by id"); ;
        }

        public async Task<IUser> GetUserFromContextAsync(HttpContext context)
        {
            var id = GetUserIdFromContextAsync(context);
            return await _rep.Users.GetUserAsync(id);
        }
        public Guid GetUserIdFromContextAsync(HttpContext context)
        {
            var claims = context.User.Claims.ToList();
            if (claims.Count == 0)
                throw new AccessDeniedException("not found id in claims");
            return new Guid(claims[0].Value.ToString());
        }

        public async Task<IUser> AuthenticateAsync(LoginDetails details)
        {
            var user = await _rep.Users.LoginAsync(details) ??
                throw new AccessDeniedException("user not found by login details");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
                {
                new Claim("id", user.Id.ToString())
                };
            // Генерируем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),  // действие токена истекает через 30 минут
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            user.Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return user;

        }

        public async Task<IUser> RegisterAsync(LoginDetails details)
        {
            if (!_rep.Users.IsUsernameNotTakenAsync(details.Name).Result)
                throw new ArgumentException("username is taken");
            var user = new User(details.Name, details.Password);
            _validator.ValidateAndThrow(user);
            await _rep.Users.PostUserAsync(user);
            return AuthenticateAsync(details).Result;

        }

        public async Task<bool> IsUsernameNotTakenAsync(string username)
        {
            return await _rep.Users.IsUsernameNotTakenAsync(username);
        }
    }
}
