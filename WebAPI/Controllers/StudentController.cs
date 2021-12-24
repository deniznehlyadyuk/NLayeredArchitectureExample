using System;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Business.DTOs.Student;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var result = await _studentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentCreateDto input)
        {
            var resultEntityDto = await _studentService.AddAsync(input);
            return Ok(resultEntityDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentUpdateDto input)
        {
            var resultEntityDto = await _studentService.UpdateAsync(id, input);
            return Ok(resultEntityDto);
        }
      
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            await _studentService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}