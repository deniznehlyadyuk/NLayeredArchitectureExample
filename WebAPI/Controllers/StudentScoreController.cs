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
      
        [HttpGet("/{studentId:guid}/StudentAllLessonAverage")]
        public async Task<IActionResult> StudentAllLessonAverage(Guid studentId)
        {
            var result = await _scoreService.StudentAllLessonAverage(studentId);
            return Ok(result);
        }
        [HttpGet("/{studentId:guid}/StudentGeneralAverage")]
        public async Task<IActionResult> StudentGeneralAverage(Guid studentId)
        {
            var result = await _scoreService.StudentGeneralAverage(studentId);
            return Ok(result);
        }
        [HttpGet("/{lessonId:guid}/GreatestStudentInOneLesson")]
        public async Task<IActionResult> GreatestStudentInOneLesson(Guid lessonId)
        {
            var result = await _scoreService.GreatestStudentInOnelesson( lessonId);
            return Ok(result);
        }
        [HttpGet("/{lessonId:guid}/WorstStudentInOneLesson")]
        public async Task<IActionResult> WorstStudentInOneLesson( Guid lessonId)
        {
            var result = await _scoreService.WorstStudentInOneLesson(lessonId);
            return Ok(result);
        }
    }
}