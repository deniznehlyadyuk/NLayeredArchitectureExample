using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Student;
using Core.DataAccess;
using Core.Domain;
using Core.Utils.Results;
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
            
            try
            {
                await BaseEntityRepository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TEntityGetDto>(ex.Message);
            }
            
            var entityDto = Mapper.Map<TEntity, TEntityGetDto>(entity);
            return new SuccessDataResult<TEntityGetDto>(entityDto);
        }

        public virtual async Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li {typeof(TEntity).Name} entitysi bulunamadı.");
            }

            var updatedEntity = Mapper.Map(input, entity);

            try
            {
                await BaseEntityRepository.UpdateAsync(updatedEntity);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TEntityGetDto>(ex.Message);
            }

            var entityDto = Mapper.Map<TEntity, TEntityGetDto>(updatedEntity);
            
            return new SuccessDataResult<TEntityGetDto>(entityDto);
        }

        public virtual async Task<IResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await BaseEntityRepository.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }

            return new SuccessResult($"'{id}' id'li {typeof(TEntity).Name} entitysi silindi.");
        }

        public virtual async Task<IDataResult<TEntityGetDto>> GetByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li {typeof(TEntity).Name} entitysi bulunamadı.");
            }

            var entityDto = Mapper.Map<TEntity, TEntityGetDto>(entity);
            
            return new SuccessDataResult<TEntityGetDto>(entityDto);
        }

        public virtual async Task<IDataResult<ICollection<TEntityGetDto>>> GetAllAsync()
        {
            var entities = await BaseEntityRepository.GetListAsync();
            var entityDtos = Mapper.Map<List<TEntity>, List<TEntityGetDto>>(entities.ToList());
            return new SuccessDataResult<ICollection<TEntityGetDto>>(entityDtos);
        }
    }
}