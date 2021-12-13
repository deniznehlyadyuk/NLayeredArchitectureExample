using Business.Utils.Abstract;
using Core.Business.DTOs.Doctor;

namespace Business.Abstract
{
    public interface IDoctorService : ICrudEntityService<DoctorGetDto, DoctorCreateDto, DoctorUpdateDto>
    {
        
    }
}