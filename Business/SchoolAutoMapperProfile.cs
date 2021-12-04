using AutoMapper;
using Core.Business.DTOs.Student;
using Domain;

namespace Business
{
    public class SchoolAutoMapperProfile : Profile
    {
        public SchoolAutoMapperProfile()
        {
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentGetDto>();
        }
    }
}