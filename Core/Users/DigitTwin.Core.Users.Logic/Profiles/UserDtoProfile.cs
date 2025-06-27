using AutoMapper;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Misc.Tools;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Профиль для сущностей <see cref="UserCreateDto"/> и <see cref="UserDto"/>
    /// </summary>
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile() 
        {
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (!string.IsNullOrEmpty(src.Password))
                    {
                        PasswordHasherTool.CreatePasswordHash(src.Password, out byte[] passwordHash, out byte[] passwordSalt);
                        dest.Password = passwordHash;
                        dest.PasswordSalt = passwordSalt;
                    }
                });

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
