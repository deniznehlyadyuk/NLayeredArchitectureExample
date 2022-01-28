using Business.Utils;
using Core.Business.DTOs.StudentScore;

namespace Business.Abstract
{
    public interface IStudentScoreService : ICrudEntityService<StudentScoreGetDto, StudentScoreCreateDto, StudentScoreUpdateDto>
    {
        
    }
}