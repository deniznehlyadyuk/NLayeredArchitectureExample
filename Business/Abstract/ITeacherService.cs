using Business.Utils;
using Core.Business.DTOs.Teacher;

namespace Business.Abstract
{
    public interface ITeacherService : ICrudEntityService<TeacherGetDto,TeacherCreateDto,TeacherUpdateDto>
    {
        
    }
}