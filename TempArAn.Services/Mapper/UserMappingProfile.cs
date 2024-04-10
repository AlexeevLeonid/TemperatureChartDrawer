using AutoMapper;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Responses;

namespace TempArAn.Services.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<IUser, UserResponse>();
        }
    }
}
