using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Utils.Abstract;
using Core.Business.DTOs;
using Core.Business.DTOs.Abstract;
using Core.DataAccess;
using Core.Domain;
using Core.Utils.Result;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Business.Utils
{
    public class BaseEntityManager<TEntity, TEntityGetDto> : IBaseEntityService<TEntityGetDto>
        where TEntity : BaseEntity, new()
        where TEntityGetDto : IEntityGetDto, new()
    {
        protected readonly IUnitOfWorks UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IEntityRepository<TEntity> BaseEntityRepository;

        public BaseEntityManager(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            BaseEntityRepository = unitOfWork.GenerateRepository<TEntity>();
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

        public async Task<IDataResult<ICollection<TEntityGetDto>>> GetListAsync()
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

        protected TEntity SetExtraProperties(TEntity entity, IDictionary<string, object> extraProperties = null)
        {
            if (extraProperties != null)
            {
                foreach (var propertyName in extraProperties.Keys)
                {
                    var property = entity.GetType().GetProperty(propertyName);

                    if (property == null) // entity'nin böyle bir propertysi yoksa
                    {
                        continue;
                    }

                    var castedValue = Convert.ChangeType(extraProperties[propertyName], property.PropertyType);
                    property.SetValue(entity, castedValue, null);
                }
            }
            
            return entity;
        }
    }
}