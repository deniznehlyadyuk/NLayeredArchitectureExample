using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Utils.Abstract;
using Core.Business.DTOs.SecretaryDoctor;
using Core.Utils.Result;

namespace Business.Abstract
{
    public interface ISecretaryDoctorService : ICrudEntityService<SecretaryDoctorGetDto, SecretaryDoctorCreateDto, SecretaryDoctorCreateDto>
    {
        Task<IDataResult<ICollection<SecretaryDoctorGetDto>>> GetListBySecretaryId(Guid id);
    }
}