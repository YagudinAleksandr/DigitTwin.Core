using AutoMapper;
using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.Users.Logic.Profiles
{
    /// <summary>
    /// Профиль для сущностей <see cref="OrganizationCreateRequestDto"/> и <see cref="OrganizationDto"/>
    /// </summary>
    public class OrganizationDtoProfile : Profile
    {
        public OrganizationDtoProfile() 
        {
            CreateMap<OrganizationCreateRequestDto, Organization>();
            CreateMap<OrganizationDto, Organization>().ReverseMap();
        }
    }
}
