using Api.EndPoint.Models.Dtos;
using AutoMapper;
using Domain.Users;

namespace Api.EndPoint.MappingProfiles
{
    public class UserEndPointMappingProfile: Profile
    {
        public UserEndPointMappingProfile()
        {
            CreateMap<User, ReturnLoginViewModel>().ReverseMap();
            CreateMap<User, AccountDto>().ReverseMap();
        }
    }
}
