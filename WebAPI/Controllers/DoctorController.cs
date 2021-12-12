using Business.Abstract;
using Core.Business.DTOs.Doctor;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class DoctorController : EntityController<DoctorGetDto, DoctorCreateDto, DoctorUpdateDto>
    {
        public DoctorController(IDoctorService service) : base(service)
        {
        }
    }
}