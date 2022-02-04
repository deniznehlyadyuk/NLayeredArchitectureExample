using System;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Business.DTOs.StudentScore;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class StudentScoreController : CrudEntityController<StudentScoreGetDto, StudentScoreCreateDto, StudentScoreUpdateDto>
    {
        private readonly IStudentScoreService _scoreService;
        public StudentScoreController(IStudentScoreService service, IStudentScoreService scoreService) : base(service)
        {
            _scoreService = scoreService;
        }
        
        [HttpGet("/{lessonId:guid}/GeneralAverage")]
        public async Task<IActionResult> GetGeneralAverageLesson(Guid lessonId)
        {
            var result = await _scoreService.GeneralAverageLesson(lessonId);
            return Ok(result);
        }
    }
}