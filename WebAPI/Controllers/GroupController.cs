using System;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Group;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class GroupController : CrudEntityController<GroupGetDto,GroupCreateDto,GroupUpdateDto>
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService service, IGroupService groupService) : base(service)
        {
            _groupService = groupService;
        }
        
        [HttpGet("/{teacherId:guid}/GetStudentsForTeacher")]
        public async Task<IActionResult> GetStudentsForTeacher(Guid teacherId)
        {
            var result = await _groupService.GetStudentsForTeacher(teacherId);
            return Ok(result);
        }
        [HttpGet("/{studentId:guid}/GetTeachersForStudents")]
        public async Task<IActionResult> GetTeachersForStudents(Guid studentId)
        {
            var result = await _groupService.GetTeachersForStudent(studentId);
            return Ok(result);
        }
    }
}