using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Lesson;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class LessonManager : CrudEntityManager<Lesson, LessonGetDto, LessonDto, LessonDto>, ILessonService
    {
        public LessonManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}