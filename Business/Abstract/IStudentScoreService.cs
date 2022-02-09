using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Utils;
using Core.Business.DTOs.StudentScore;
using Core.Utils.Results;

namespace Business.Abstract
{
    public interface IStudentScoreService : ICrudEntityService<StudentScoreGetDto, StudentScoreCreateDto, StudentScoreUpdateDto>
    {
        public Task<IDataResult<float>> GeneralAverageLesson(Guid lesson_id);
     
        public Task<IDataResult<float>> StudentAllLessonAverage(Guid studentId);
        public Task<IDataResult<float>> StudentGeneralAverage(Guid studentId);
        public Task<IDataResult<StudentScoreGetDto>> GreatestStudentInOnelesson(Guid lessonId);
        public Task<IDataResult<StudentScoreGetDto>> WorstStudentInOneLesson(Guid lessonId);
      
    }
}