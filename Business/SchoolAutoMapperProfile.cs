using AutoMapper;
using Core.Business.DTOs.Group;
using Core.Business.DTOs.Lesson;
using Core.Business.DTOs.Student;
using Core.Business.DTOs.StudentScore;
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
                .ForMember(x => x.IdentityNumber, x => x.MapFrom(y => y.Person.IdentityNumber))
                .ForMember(x => x.LessonName, x => x.MapFrom(y => y.Lesson.Name));
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<TeacherUpdateDto, Teacher>();
            CreateMap<TeacherCreateDto, Person>();
            CreateMap<TeacherUpdateDto, Person>();

            // Lesson
            CreateMap<Lesson, LessonGetDto>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<LessonDto, Lesson>();
            
            // StudentScore
            CreateMap<StudentScore, StudentScoreGetDto>()
                .ForMember(x => x.LessonName, x => x.MapFrom(y => y.Lesson.Name))
                .ForMember(x => x.StudentName, x => x.MapFrom(y => y.Student.Person.FullName));
            CreateMap<StudentScoreCreateDto, StudentScore>();
            CreateMap<StudentScoreUpdateDto, StudentScore>();
            // Group
            CreateMap<GroupCreateDto, Group>();
            CreateMap<Group, GroupGetDto>()
                .ForMember(x => x.LessonName, x => x.MapFrom(y => y.Lesson.Name))
                .ForMember(x => x.StudentName, x => x.MapFrom(y => y.Student.Person.FullName))
                .ForMember(x => x.TeacherName, x => x.MapFrom(y => y.Teacher.Person.FullName));
        }
    }
}