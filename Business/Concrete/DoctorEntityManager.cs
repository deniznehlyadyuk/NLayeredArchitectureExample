using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Doctor;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class DoctorEntityManager : EmployeeEntityManager<Doctor, DoctorGetDto, DoctorCreateDto, DoctorUpdateDto>, IDoctorService
    {
        public DoctorEntityManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }
    }
}