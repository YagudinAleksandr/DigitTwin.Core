using AutoMapper;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Профиль для сущностей <see cref="UserCreateDto"/> и <see cref="UserDto"/>
    /// </summary>
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile() 
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
