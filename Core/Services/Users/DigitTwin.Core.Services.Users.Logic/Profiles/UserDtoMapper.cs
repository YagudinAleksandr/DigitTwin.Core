using AutoMapper;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Профилирование DTO <see cref="UserDto"/> и <see cref="UserCreateDto"/> на <see cref="User"/>
    /// </summary>
    internal class UserDtoMapper : Profile
    {
        public UserDtoMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
        }
    }
}
