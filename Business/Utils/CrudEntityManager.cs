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
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
        protected bool BeginTransactionFlag;
        protected bool RollbackTransactionFlag;
        protected bool CommitTransactionFlag;

        public CrudEntityManager(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            BaseEntityRepository = UnitOfWork.GenerateRepository<TEntity>();
            BeginTransactionFlag = false;
            RollbackTransactionFlag = false;
            CommitTransactionFlag = false;
        }

        public virtual async Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var entity = Mapper.Map<TEntityCreateDto, TEntity>(input);

            if (BeginTransactionFlag)
            {
                await UnitOfWork.BeginTransactionAsync();
            }

            if (extraProperties != null)
            {
                foreach (var key in extraProperties.Keys)
                {
                    var property = entity.GetType().GetProperty(key);

                    if (property == null)
                    {
                        continue;
                    }

                    var castedValue = Convert.ChangeType(extraProperties[key], property.PropertyType);
                    property.SetValue(entity, castedValue, null);
                }
            }

            try
            {
                await BaseEntityRepository.AddAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                if (RollbackTransactionFlag)
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

            if (extraProperties != null)
            {
                foreach (var key in extraProperties.Keys)
                {
                    var property = entity.GetType().GetProperty(key);

                    if (property == null)
                    {
                        continue;
                    }

                    var castedValue = Convert.ChangeType(extraProperties[key], property.PropertyType);
                    property.SetValue(updatedEntity, castedValue, null);
                }
            }
            
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
                if (RollbackTransactionFlag)
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
                if (RollbackTransactionFlag)
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

        protected virtual async Task<TEntityGetDto> ConvertToDtoForGetAsync(TEntity input)
        {
            return Mapper.Map<TEntity, TEntityGetDto>(input);
        }
        
        protected IDataResult<TEntityGetDto> GenerateErrorMessage(DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException is PostgresException)
            {
                var postgresEx = (PostgresException) ex.InnerException;
                    
                switch (postgresEx.SqlState)
                {
                    case "23502":
                        return new ErrorDataResult<TEntityGetDto>($"'{postgresEx.TableName}' tablosunun '{postgresEx.ColumnName}' sütunu null değere eşit olamaz.");
                    case "23505":
                        return new ErrorDataResult<TEntityGetDto>($"'{postgresEx.TableName}' tablosu için girilen değerler içerisinden unique özelliğe sahip olanlardan biri veya birçoğu veritabanında zaten mevcut.");
                }
            }

            return new ErrorDataResult<TEntityGetDto>(ex.Message);
        }
    }
}