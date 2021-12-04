using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Student;
using Core.DataAccess;
using Core.Domain;
using DataAccess;

namespace Business.Utils
{
    public class CrudEntityManager<TEntity, TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> : ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto>
        where TEntity : BaseEntity, new()
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWorks UnitOfWork;
        protected readonly IEntityRepository<TEntity> BaseEntityRepository;

        public CrudEntityManager(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            BaseEntityRepository = UnitOfWork.GenerateRepository<TEntity>();
        }

        public virtual async Task<TEntityGetDto> AddAsync(TEntityCreateDto input)
        {
            var entity = Mapper.Map<TEntityCreateDto, TEntity>(input);
            await BaseEntityRepository.AddAsync(entity);
            return Mapper.Map<TEntity, TEntityGetDto>(entity);
        }

        public virtual async Task<TEntityGetDto> UpdateAsync(Guid id, TEntityUpdateDto input)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new TEntityGetDto();
            }

            var updatedEntity = Mapper.Map(input, entity);

            await BaseEntityRepository.UpdateAsync(updatedEntity);

            return Mapper.Map<TEntity, TEntityGetDto>(updatedEntity);
        }

        public virtual async Task DeleteByIdAsync(Guid id)
        {
            await BaseEntityRepository.DeleteByIdAsync(id);
        }

        public async Task<TEntityGetDto> GetByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);
            return await ConvertToDtoForGetAsync(entity);
        }

        public async Task<ICollection<TEntityGetDto>> GetAllAsync()
        {
            var entities = await BaseEntityRepository.GetListAsync();
            var entityGetDtos = new List<TEntityGetDto>();

            foreach (var entity in entities.ToList())
            {
                entityGetDtos.Add(await ConvertToDtoForGetAsync(entity));
            }

            return entityGetDtos;
        }

        public virtual async Task<TEntityGetDto> ConvertToDtoForGetAsync(TEntity input)
        {
            return Mapper.Map<TEntity, TEntityGetDto>(input);
        }
    }
}