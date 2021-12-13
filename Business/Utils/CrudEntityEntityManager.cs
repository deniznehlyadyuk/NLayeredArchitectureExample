using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Utils.Abstract;
using Core.Business.DTOs.Abstract;
using Core.Domain;
using Core.Utils.Result;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Business.Utils
{
    public class CrudEntityEntityManager<TEntity, TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> : 
        BaseEntityManager<TEntity, TEntityGetDto>,
        ICrudEntityService<TEntityGetDto, TEntityCreateDto, TEntityUpdateDto>
        
        where TEntity : BaseEntity, new()
        where TEntityGetDto : IEntityGetDto, new()
        where TEntityCreateDto : IDto, new()
        where TEntityUpdateDto : IDto, new()
    {
        protected bool BeginTransactionFlag;
        protected bool CommitTransactionFlag;
        protected bool RollbackTransacitonFlag;
        
        public CrudEntityEntityManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            BeginTransactionFlag = false;
            CommitTransactionFlag = false;
            RollbackTransacitonFlag = false;
        }

        public virtual async Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var entity = Mapper.Map<TEntityCreateDto, TEntity>(input);

            entity = SetExtraProperties(entity, extraProperties);

            if (BeginTransactionFlag)
            {
                await UnitOfWork.BeginTransactionAsync();
            }
            
            try
            {
                await BaseEntityRepository.AddAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                if (RollbackTransacitonFlag)
                {
                    await UnitOfWork.RollbackTransactionAsync();
                }
                
                return GenerateErrorMessage(ex);
            }
            
            if (CommitTransactionFlag)
            {
                await UnitOfWork.CommitTransactionAsync();
            }

            return await GetByIdAsync(entity.Id);
        }

        public virtual async Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input, IDictionary<string, object> extraProperties = null)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li '{typeof(TEntity).Name}' entity'si bulunamadı.");
            }

            var updatedEntity = Mapper.Map(input, entity);

            updatedEntity = SetExtraProperties(updatedEntity, extraProperties);
            
            if (BeginTransactionFlag)
            {
                await UnitOfWork.BeginTransactionAsync();
            }
            
            try
            {
                await BaseEntityRepository.UpdateAsync(updatedEntity);
            }
            catch (DbUpdateException ex)
            {
                if (RollbackTransacitonFlag)
                {
                    await UnitOfWork.RollbackTransactionAsync();
                }

                return GenerateErrorMessage(ex);
            }
            
            if (CommitTransactionFlag)
            {
                await UnitOfWork.CommitTransactionAsync();
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

            if (BeginTransactionFlag)
            {
                await UnitOfWork.BeginTransactionAsync();
            }
            
            try
            {
                await BaseEntityRepository.DeleteAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                if (RollbackTransacitonFlag)
                {
                    await UnitOfWork.RollbackTransactionAsync();
                }

                return GenerateErrorMessage(ex);
            }
            
            if (CommitTransactionFlag)
            {
                await UnitOfWork.CommitTransactionAsync();
            }


            return new SuccessResult($"'{typeof(TEntity).Name}' entitysi silindi.");
        }
    }
}