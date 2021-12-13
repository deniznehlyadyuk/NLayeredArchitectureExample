using System;
using System.Threading.Tasks;
using Business.Utils.Abstract;
using Core.Business.DTOs.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Abstract
{
    [ApiController]
    [Route("/api/[controller]")]
    public abstract class EntityController<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        private readonly ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> _service;

        public EntityController(ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TEntityCreateDto input)
        {
            var result = await _service.AddAsync(input);

            if (!result.Success)
            {
                return StatusCode(406, result);
            }

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TEntityUpdateDto input)
        {
            var result = await _service.UpdateAsync(id, input);

            if (!result.Success)
            {
                return StatusCode(406, result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteByIdAsync(id);

            if (!result.Success)
            {
                return StatusCode(406, result);
            }

            return Ok(result);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetListAsync();
            return Ok(result);
        }
    }
}