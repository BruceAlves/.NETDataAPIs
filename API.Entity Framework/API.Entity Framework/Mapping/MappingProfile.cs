using API.Entity_Framework.Dtos;
using API.Entity_Framework.Models;
using AutoMapper;

namespace API.Entity_Framework.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
