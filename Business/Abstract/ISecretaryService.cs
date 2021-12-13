using Business.Utils.Abstract;
using Core.Business.DTOs.Secretary;

namespace Business.Abstract
{
    public interface ISecretaryService : ICrudEntityService<SecretaryGetDto, SecretaryCreateDto, SecretaryUpdateDto>
    {
        
    }
}