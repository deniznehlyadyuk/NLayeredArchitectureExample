using Business.Abstract;
using Core.Business.DTOs.Student;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class StudentController : CrudEntityController<StudentGetDto,StudentCreateDto,StudentUpdateDto>
    {
        public StudentController(IStudentService service) : base(service)
        {
        }
    }
}