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
    }
}