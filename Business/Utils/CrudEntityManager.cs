using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.DTOs;
using Core.DataAccess;
using Core.Domain;
using Core.Utils.Result;
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

        public virtual async Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input)
        {
            var entity = Mapper.Map<TEntityCreateDto, TEntity>(input);
            await BaseEntityRepository.AddAsync(entity);
            var entityGetDto = Mapper.Map<TEntity, TEntityGetDto>(entity);
            return new SuccessDataResult<TEntityGetDto>(entityGetDto);
        }

        public virtual async Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li '{typeof(TEntity).Name}' entity'si bulunamadı.");
            }

            var updatedEntity = Mapper.Map(input, entity);

            await BaseEntityRepository.UpdateAsync(updatedEntity);

            var entityGetDto = Mapper.Map<TEntity, TEntityGetDto>(updatedEntity);

            return new SuccessDataResult<TEntityGetDto>(entityGetDto);
        }

        public virtual async Task<IResult> DeleteByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x=>x.Id == id);

            if (entity == null)
            {
                return new ErrorResult($"'{id}' id'li '{typeof(TEntity).Name}' entitysi bulunamadı.");
            }

            await BaseEntityRepository.DeleteAsync(entity);

            return new SuccessResult($"'{typeof(TEntity).Name}' entitysi silindi.");
        }

        public async Task<IDataResult<TEntityGetDto>> GetByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li '{typeof(TEntity).Name}' entity'si bulunamadı.");
            }
            
            var entityGetDto = await ConvertToDtoForGetAsync(entity);
            return new SuccessDataResult<TEntityGetDto>(entityGetDto);
        }

        public async Task<IDataResult<ICollection<TEntityGetDto>>> GetAllAsync()
        {
            var entities = await BaseEntityRepository.GetListAsync();
            var entityGetDtos = new List<TEntityGetDto>();

            foreach (var entity in entities.ToList())
            {
                entityGetDtos.Add(await ConvertToDtoForGetAsync(entity));
            }

            return new SuccessDataResult<ICollection<TEntityGetDto>>(entityGetDtos);
        }

        public virtual async Task<TEntityGetDto> ConvertToDtoForGetAsync(TEntity input)
        {
            return Mapper.Map<TEntity, TEntityGetDto>(input);
        }
    }
}