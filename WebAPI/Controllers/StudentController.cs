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
    }
}