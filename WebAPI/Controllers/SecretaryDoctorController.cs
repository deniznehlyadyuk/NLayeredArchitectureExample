using System;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Business.DTOs.SecretaryDoctor;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers
{
    public class SecretaryDoctorController : EntityController<SecretaryDoctorGetDto, SecretaryDoctorCreateDto, SecretaryDoctorCreateDto>
    {
        private readonly ISecretaryDoctorService _service;
        
        public SecretaryDoctorController(ISecretaryDoctorService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("Secretary/{id:guid}")]
        public async Task<IActionResult> GetListBySecretaryId(Guid id)
        {
            var result = await _service.GetListBySecretaryId(id);
            return Ok(result);
        }
    }
}