using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Secretary;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class SecretaryEntityManager : EmployeeEntityManager<Secretary, SecretaryGetDto, SecretaryCreateDto, SecretaryUpdateDto>, ISecretaryService
    {
        public SecretaryEntityManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}