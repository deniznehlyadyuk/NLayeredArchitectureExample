using Business.Utils;
using Core.Business.DTOs.Student;

namespace Business.Abstract
{
    public interface IStudentService : ICrudEntityService<StudentGetDto, StudentDto>
    {
        
    }
}