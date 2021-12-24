using System;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Lesson;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson([FromBody] LessonDto input)
        {
            var resultEntityDto = await _lessonService.AddAsync(input);
            return Ok(resultEntityDto);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLesson(Guid id)
        {
            var result = await _lessonService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLessons()
        {
            var result = await _lessonService.GetAllAsync();
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            await _lessonService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLesson(Guid id,LessonDto input)
        {
            var result = await _lessonService.UpdateAsync(id, input);
            return Ok(result);
        }

    }
}