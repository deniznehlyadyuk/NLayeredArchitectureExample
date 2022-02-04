using System;
using System.Threading.Tasks;
using Business.Utils;
using Core.Business.DTOs.StudentScore;
using Core.Utils.Results;

namespace Business.Abstract
{
    public interface IStudentScoreService : ICrudEntityService<StudentScoreGetDto, StudentScoreCreateDto, StudentScoreUpdateDto>
    {
        public Task<float> GeneralAverageLesson(Guid lesson_id);
    }
}