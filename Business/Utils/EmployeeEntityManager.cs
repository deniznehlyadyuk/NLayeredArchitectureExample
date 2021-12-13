using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Employee;
using Core.Domain;
using Core.Utils.Result;
using DataAccess;

namespace Business.Utils
{
    public class EmployeeEntityManager<TEntity, TEntityGetDto, TEntityCreateDto, TEntityUpdateDto> : 
        CrudEntityEntityManager<TEntity, TEntityGetDto, TEntityCreateDto, TEntityUpdateDto>

        where TEntity : BaseEntity, IEmployeeEntity, new()
        where TEntityGetDto : IEmployeeEntityGetDto, new()
        where TEntityCreateDto : IEmployeeEntityCreateDto, new()
        where TEntityUpdateDto : IEmployeeEntityUpdateDto, new()
    {
        private readonly EmployeeHelper _employeeHelper;
        
        public EmployeeEntityManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _employeeHelper = new EmployeeHelper(unitOfWork, mapper);
            BeginTransactionFlag = false;
            CommitTransactionFlag = true;
            RollbackTransacitonFlag = true;
        }

        protected override async Task<TEntityGetDto> ConvertToDtoForGetAsync(TEntity input)
        {
            var employeeResult = await _employeeHelper.GetByIdAsync(input.EmployeeId);
            var entityGetDto = Mapper.Map<TEntity, TEntityGetDto>(input);
            entityGetDto.EmployeeInfo = Mapper.Map<EmployeeGetDto, EmployeeCreateDto>(employeeResult.Data);
            return entityGetDto;
        }

        public override async Task<IDataResult<TEntityGetDto>> AddAsync(TEntityCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            await UnitOfWork.BeginTransactionAsync();
            
            var employeeResult = await _employeeHelper.AddAsync(input.EmployeeInfo);

            if (!employeeResult.Success)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<TEntityGetDto>(employeeResult.Message);
            }

            var employeeId = employeeResult.Data.Id;
            
            if (extraProperties == null)
            {
                extraProperties = new Dictionary<string, object>();
            }

            extraProperties["EmployeeId"] = employeeId;
            
            return await base.AddAsync(input, extraProperties);
        }

        public override async Task<IDataResult<TEntityGetDto>> UpdateAsync(Guid id, TEntityUpdateDto input, IDictionary<string, object> extraProperties = null)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li '{typeof(TEntity).Name}' entity'si bulunamadı.");
            }
            
            var employeeGetResult = await _employeeHelper.GetByIdAsync(entity.EmployeeId);

            if (!employeeGetResult.Success)
            {
                return new ErrorDataResult<TEntityGetDto>($"'{id}' id'li '{typeof(TEntity).Name}' entitysinin employee'si bulunamadı.");
            }

            var employee = employeeGetResult.Data;

            await UnitOfWork.BeginTransactionAsync();
            
            var employeeUpdateResult = await _employeeHelper.UpdateAsync(employee.Id, input.EmployeeInfo);

            if (!employeeUpdateResult.Success)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<TEntityGetDto>(employeeUpdateResult.Message);
            }
            
            if (extraProperties == null)
            {
                extraProperties = new Dictionary<string, object>();
            }

            extraProperties["EmployeeId"] = employee.Id;
            
            return await base.UpdateAsync(id, input, extraProperties);
        }

        public override async Task<IResult> DeleteByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetAsync(x => x.Id == id);
            var employeeGetResult = await _employeeHelper.GetByIdAsync(entity.EmployeeId);
            
            if (!employeeGetResult.Success)
            {
                return employeeGetResult;
            }

            return await _employeeHelper.DeleteEmployee(employeeGetResult.Data.Id);
        }
    }
}