using System.Threading.Tasks;
using Business.Abstract;
using Core.Business.DTOs.Teacher;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class TeacherController : CrudEntityController<TeacherGetDto, TeacherCreateDto, TeacherUpdateDto>
    {
        private readonly ITeacherService _service;
        public TeacherController(ITeacherService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("greatest-teacher-for-average")]
        public async Task<IActionResult> GreatestTeacherForAverage()
        {
            var result = await _service.GreatestTeacherForAverage();
            return Ok(result);
        }

        [HttpGet("worst-teacher-for-average")]
        public async Task<IActionResult> WorstTeacherForAverage()
        {
            var result = await _service.WorstTeacherForAverage();
            return Ok(result);
        }
    }
}