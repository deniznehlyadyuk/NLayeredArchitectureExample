using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.SecretaryDoctor;
using Core.Utils.Result;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class SecretaryDoctorManager :
        CrudEntityEntityManager<SecretaryDoctor, SecretaryDoctorGetDto, SecretaryDoctorCreateDto, SecretaryDoctorCreateDto>,
        ISecretaryDoctorService
    {
        public SecretaryDoctorManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override async Task<SecretaryDoctorGetDto> ConvertToDtoForGetAsync(SecretaryDoctor input)
        {
            var secretaryDoctor = await UnitOfWork.SecretaryDoctorRepository.GetWithInclude(input.Id);
            return Mapper.Map<SecretaryDoctor, SecretaryDoctorGetDto>(secretaryDoctor);
        }

        public override async Task<IDataResult<SecretaryDoctorGetDto>> AddAsync(SecretaryDoctorCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var status = await AddUpdateControl(input);

            if (!status.Success)
            {
                return new ErrorDataResult<SecretaryDoctorGetDto>(status.Message);
            }
            
            return await base.AddAsync(input, extraProperties);
        }

        public override async Task<IDataResult<SecretaryDoctorGetDto>> UpdateAsync(Guid id, SecretaryDoctorCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var status = await AddUpdateControl(input);

            if (!status.Success)
            {
                return new ErrorDataResult<SecretaryDoctorGetDto>(status.Message);
            }
            
            return await base.UpdateAsync(id, input, extraProperties);
        }

        public async Task<IDataResult<ICollection<SecretaryDoctorGetDto>>> GetListBySecretaryId(Guid id)
        {
            var secretaryDoctors = await UnitOfWork.SecretaryDoctorRepository.GetWithInclude(x => x.SecretaryId == id);
            var secretaryDoctorDtos = Mapper.Map<List<SecretaryDoctor>, List<SecretaryDoctorGetDto>>(secretaryDoctors.ToList());
            return new SuccessDataResult<ICollection<SecretaryDoctorGetDto>>(secretaryDoctorDtos);
        }
        
        private async Task<IResult> AddUpdateControl(SecretaryDoctorCreateDto input)
        {
            var isDoctorAlreadyInAnySecretary = await UnitOfWork.SecretaryDoctorRepository.AnyAsync(x => x.DoctorId == input.DoctorId);

            if (isDoctorAlreadyInAnySecretary)
            {
                return new ErrorResult("İlgili doktor zaten bir sekreter ile bağlanmış.");
            }

            var secretaryDoctorCount = await UnitOfWork.SecretaryDoctorRepository.CountAsync(x => x.SecretaryId == input.SecretaryId && x.DoctorId != input.DoctorId);

            if (secretaryDoctorCount > 5)
            {
                return new ErrorResult("Bir sekreter beş adetten daha fazla doktor ile bağlanamaz.");
            }

            return new SuccessResult();
        }
    }
}