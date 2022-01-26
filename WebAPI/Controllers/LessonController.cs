using Business.Abstract;
using Core.Business.DTOs.Lesson;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class LessonController : CrudEntityController<LessonGetDto, LessonDto, LessonDto>
    {
        public LessonController(ILessonService service) : base(service)
        {
        }
    }
}