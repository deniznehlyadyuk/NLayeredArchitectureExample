using AutoMapper;
using Core.Business.DTOs.Lesson;
using Core.Business.DTOs.Student;
using Domain;

namespace Business
{
    public class SchoolAutoMapperProfile : Profile
    {
        public SchoolAutoMapperProfile()
        {
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentGetDto>();
            CreateMap<StudentCreateDto, Person>();
            CreateMap<StudentCreateDto, StudentGetDto>();
            
            CreateMap<LessonDto, Lesson>();
            CreateMap<Lesson, LessonGetDto>();
        }
    }
}