using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.StudentScore;
using Core.Utils.Results;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class StudentScoreManager : CrudEntityManager<StudentScore, StudentScoreGetDto, StudentScoreCreateDto, StudentScoreUpdateDto>, IStudentScoreService
    {
        
        public StudentScoreManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        public override async Task<IDataResult<StudentScoreGetDto>> AddAsync(StudentScoreCreateDto input)
        {  
            
            var entity = Mapper.Map<StudentScoreCreateDto, StudentScore>(input);
            
            var scores = await BaseEntityRepository.GetListAsync(x => x.StudentId == input.StudentId);

            foreach (var score in scores)
            {
                if (score.ExamNumber==input.ExamNumber && score.LessonId == input.LessonId)
                {
                    return new ErrorDataResult<StudentScoreGetDto>("Sınav Numarası aynı olamaz");
                }
            }
            
            await BaseEntityRepository.AddAsync(entity);

            return await GetByIdAsync(entity.Id);
        }

        public override async Task<IDataResult<ICollection<StudentScoreGetDto>>> GetAllAsync()
        {
            var studentScores = await BaseEntityRepository.GetListWithIncludeAsync(null, x => x.Student, 
                x => x.Student.Person, x => x.Lesson);
            var studentScoreDtos = Mapper.Map<List<StudentScore>, List<StudentScoreGetDto>>(studentScores.ToList());
            return new SuccessDataResult<ICollection<StudentScoreGetDto>>(studentScoreDtos);
        }

   

        public async Task<float> GeneralAverageLesson(Guid lesson_id)
        {
            var studentScores = await BaseEntityRepository.GetListAsync(x => x.LessonId == lesson_id);
            studentScores.ToList();
            float sum = 0;
            foreach (var score in studentScores)
            {
                sum += score.Score;
            }

            return sum / studentScores.Count;
        }

        public override async Task<IDataResult<StudentScoreGetDto>> GetByIdAsync(Guid id)
        {
            var studentScore = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Student,
                x => x.Student.Person, x => x.Lesson);

            if (studentScore == null)
            {
                return new ErrorDataResult<StudentScoreGetDto>($"'{id}' id'li StudentScore entitysi bulunamadı.");
            }

            var studentScoreDto = Mapper.Map<StudentScore, StudentScoreGetDto>(studentScore);

            return new SuccessDataResult<StudentScoreGetDto>(studentScoreDto);
        }
        
        
    }
}