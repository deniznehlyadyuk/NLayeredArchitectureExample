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
    public class BaseManager<TEntity, TEntityGetDto>
        where TEntity : BaseEntity, new()
        where TEntityGetDto : IEntityGetDto, new()
    {
        protected readonly IUnitOfWorks UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IEntityRepository<TEntity> BaseEntityRepository;

        public BaseManager(IUnitOfWorks unitOfWork, IMapper mapper)
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
    }
}