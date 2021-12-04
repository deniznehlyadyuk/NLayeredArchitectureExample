using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Student;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class StudentManager : CrudEntityManager<Student, StudentGetDto, StudentDto>, IStudentService
    {
        public StudentManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}