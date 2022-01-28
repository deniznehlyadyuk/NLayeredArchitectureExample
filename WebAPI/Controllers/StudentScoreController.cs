using Business.Abstract;
using Core.Business.DTOs.StudentScore;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class StudentScoreController : CrudEntityController<StudentScoreGetDto, StudentScoreCreateDto, StudentScoreUpdateDto>
    {
        public StudentScoreController(IStudentScoreService service) : base(service)
        {
        }
    }
}