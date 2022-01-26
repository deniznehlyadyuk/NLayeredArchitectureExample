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
            // Student
            CreateMap<Student, StudentGetDto>()
                .ForMember(x => x.BornDate, x => x.MapFrom(y => y.Person.BornDate))
                .ForMember(x => x.FullName, x => x.MapFrom(y => y.Person.FullName))
                .ForMember(x => x.IdentityNumber, x => x.MapFrom(y => y.Person.IdentityNumber));
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<StudentCreateDto, Person>();
            CreateMap<StudentUpdateDto, Person>();
            
            // Teacher
            CreateMap<Teacher, TeacherGetDto>()
                .ForMember(x => x.BornDate, x => x.MapFrom(y => y.Person.BornDate))
                .ForMember(x => x.FullName, x => x.MapFrom(y => y.Person.FullName))
                .ForMember(x => x.IdentityNumber, x => x.MapFrom(y => y.Person.IdentityNumber));
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<TeacherUpdateDto, Teacher>();
            CreateMap<TeacherCreateDto, Person>();
            CreateMap<TeacherUpdateDto, Person>();

            // Lesson
            CreateMap<Lesson, LessonGetDto>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<LessonDto, Lesson>();
        }
    }
}