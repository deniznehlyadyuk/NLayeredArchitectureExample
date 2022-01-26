using System;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Business.DTOs.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(Guid id)
        {
            var result = await _teacherService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            var result = await _teacherService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherCreateDto input)
        {
            var resultEntityDto = await _teacherService.AddAsync(input);
            return Ok(resultEntityDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] TeacherUpdateDto input)
        {
            var resultEntityDto = await _teacherService.UpdateAsync(id, input);
            return Ok(resultEntityDto);
        }
      
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            await _teacherService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}