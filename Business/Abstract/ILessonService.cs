using Business.Utils;
using Core.Business.DTOs.Lesson;

namespace Business.Abstract
{
    public interface ILessonService : ICrudEntityService<LessonGetDto, LessonDto, LessonDto>
    {
        
    }
}