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
    public class CrudEntityManager<TEntity, TEntityGetDto, TEntityDto> : ICrudEntityService<TEntityGetDto, TEntityDto>
        where TEntity : BaseEntity, new()
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityDto : IDto, new()
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

        public async Task<TEntityGetDto> AddAsync(TEntityDto input)
        {
            var entity = Mapper.Map<TEntityDto, TEntity>(input);
            await BaseEntityRepository.AddAsync(entity);
            return Mapper.Map<TEntity, TEntityGetDto>(entity);
        }

        public async Task<TEntityGetDto> UpdateAsync(Guid id, TEntityDto input)
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

        public async Task DeleteByIdAsync(Guid id)
        {
            await BaseEntityRepository.DeleteByIdAsync(id);
        }

        public async Task<TEntityGetDto> GetByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);
            return Mapper.Map<TEntity, TEntityGetDto>(entity);
        }

        public async Task<ICollection<TEntityGetDto>> GetAllAsync()
        {
            var entities = await BaseEntityRepository.GetListAsync();
            return Mapper.Map<List<TEntity>, List<TEntityGetDto>>(entities.ToList());
        }
    }
}