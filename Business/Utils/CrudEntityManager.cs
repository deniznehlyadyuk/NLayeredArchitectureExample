using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.DTOs;
using Core.Domain;
using Core.Utils.Result;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Business.Utils
{
    public class CrudEntityManager<TEntity, TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> : 
        BaseManager<TEntity, TEntityGetDto>,
        ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto>
        
        where TEntity : BaseEntity, new()
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        protected readonly IUnitOfWorks UnitOfWork;

        public CrudEntityManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input)
        {
            var entity = Mapper.Map<TEntityCreateDto, TEntity>(input);

            try
            {
                await BaseEntityRepository.AddAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                return GenerateErrorMessage(ex);
            }

            return await GetByIdAsync(entity.Id);
        }

        public virtual async Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li '{typeof(TEntity).Name}' entity'si bulunamadı.");
            }

            var updatedEntity = Mapper.Map(input, entity);

            try
            {
                await BaseEntityRepository.UpdateAsync(updatedEntity);
            }
            catch (DbUpdateException ex)
            {
                return GenerateErrorMessage(ex);
            }

            return await GetByIdAsync(updatedEntity.Id);
        }

        public virtual async Task<IResult> DeleteByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x=>x.Id == id);

            if (entity == null)
            {
                return new ErrorResult($"'{id}' id'li '{typeof(TEntity).Name}' entitysi bulunamadı.");
            }

            try
            {
                await BaseEntityRepository.DeleteAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                return GenerateErrorMessage(ex);
            }

            return new SuccessResult($"'{typeof(TEntity).Name}' entitysi silindi.");
        }
    }
}