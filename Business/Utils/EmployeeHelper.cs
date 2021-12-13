using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Utils.Abstract.Base;
using Core.Business.DTOs;
using Core.Business.DTOs.Employee;
using Core.Business.DTOs.Person;
using Core.DataAccess;
using Core.Utils.Result;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Utils
{
    public class EmployeeHelper : BaseEntityManager<Employee, EmployeeGetDto>, 
        IAddEntityService<EmployeeGetDto, EmployeeCreateDto>,
        IUpdateEntityService<EmployeeGetDto, EmployeeUpdateDto>
    {
        private readonly IEntityRepository<Person> _personRepository;
        private readonly IEntityRepository<Address> _addressRepository;

        public EmployeeHelper(IUnitOfWorks unitOfWorks, IMapper mapper) : base(unitOfWorks, mapper)
        {
            _personRepository = unitOfWorks.GenerateRepository<Person>();
            _addressRepository = unitOfWorks.GenerateRepository<Address>();
        }

        protected override async Task<EmployeeGetDto> ConvertToDtoForGetAsync(Employee input)
        {
            var employee = await UnitOfWork.EmployeeRepository.GetWithInclude(input.Id);
            return Mapper.Map<Employee, EmployeeGetDto>(employee);
        }
        
        public async Task<IDataResult<EmployeeGetDto>> AddAsync(EmployeeCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var person = Mapper.Map<PersonCreateDto, Person>(input.PersonalInfo);
            var address = Mapper.Map<AddressDto, Address>(input.Address);
            var employee = Mapper.Map<EmployeeCreateDto, Employee>(input);

            var isPersonEntityExists = await _personRepository.AnyAsync(x => x.IdentityNumber == person.IdentityNumber);

            employee = SetExtraProperties(employee, extraProperties);
            
            try
            {
                if (!isPersonEntityExists)
                {
                    await _personRepository.AddAsync(person);
                }

                await _addressRepository.AddAsync(address);

                employee.AddressId = address.Id;
                employee.PersonId = person.Id;

                await BaseEntityRepository.AddAsync(employee);

                return await GetByIdAsync(employee.Id);
            }
            catch (DbUpdateException ex)
            {
                return GenerateErrorMessage(ex);
            }
        }

        public async Task<IDataResult<EmployeeGetDto>> UpdateAsync(Guid id, EmployeeUpdateDto input, IDictionary<string, object> extraProperties = null)
        {
            var employee = await UnitOfWork.EmployeeRepository.GetWithInclude(id);
            employee = SetExtraProperties(employee, extraProperties);

            var personId = employee.PersonId;
            var addressId = employee.AddressId;
            var identityNumberId = employee.Person.IdentityNumber;

            var person = Mapper.Map<PersonUpdateDto, Person>(input.PersonalInfo);
            var address = Mapper.Map<AddressDto, Address>(input.Address);

            person.Id = personId;
            person.IdentityNumber = identityNumberId;
            address.Id = addressId;
            
            try
            {
                await _personRepository.UpdateAsync(person);
                await _addressRepository.UpdateAsync(address);

                return await GetByIdAsync(employee.Id);
            }
            catch (DbUpdateException ex)
            {
                return GenerateErrorMessage(ex);
            }
        }
        
        public async Task<IResult> DeleteEmployee(Guid employeeId)
        {
            var employee = await BaseEntityRepository.GetAsync(x => x.Id == employeeId);
            try
            {
                await BaseEntityRepository.DeleteAsync(employee);
                return new SuccessResult($"'{employeeId}' id'li Employee silindi.");
            }
            catch (DbUpdateException ex)
            {
                return GenerateErrorMessage(ex);
            }
        }
    }
}