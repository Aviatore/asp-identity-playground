using AutoMapper;
using t1.Models;
using t1.Resources;

namespace t1.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSignUpResource, User>();
        }
    }
}