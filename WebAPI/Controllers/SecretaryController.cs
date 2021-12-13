using Business.Abstract;
using Core.Business.DTOs.Secretary;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class SecretaryController : EntityController<SecretaryGetDto, SecretaryCreateDto, SecretaryUpdateDto>
    {
        public SecretaryController(ISecretaryService service) : base(service)
        {
        }
    }
}