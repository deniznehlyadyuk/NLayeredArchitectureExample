using AutoMapper;
using Core.Business.DTOs.Lesson;
using Core.Business.DTOs.Student;
using Core.Business.DTOs.Teacher;
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
            CreateMap<StudentUpdateDto, Person>();
            CreateMap<StudentCreateDto, StudentGetDto>();
            CreateMap<Person, Student>();
            CreateMap<Person, StudentGetDto>();
            
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<Teacher, TeacherGetDto>();
            CreateMap<TeacherCreateDto, Person>();
            CreateMap<TeacherUpdateDto, Person>();
            CreateMap<TeacherCreateDto, TeacherGetDto>();
            CreateMap<Person, Teacher>();
            CreateMap<Person, TeacherGetDto>();
            
            CreateMap<LessonDto, Lesson>();
            CreateMap<Lesson, LessonGetDto>();
        }
    }
}