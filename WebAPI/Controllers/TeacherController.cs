using Business.Abstract;
using Core.Business.DTOs.Teacher;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class TeacherController : CrudEntityController<TeacherGetDto, TeacherCreateDto, TeacherUpdateDto>
    {
        public TeacherController(ITeacherService service) : base(service)
        {
        }
    }
}