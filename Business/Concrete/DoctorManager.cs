using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs;
using Core.Business.DTOs.Doctor;
using Core.DataAccess;
using Core.Utils.Result;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class DoctorManager : CrudEntityManager<Doctor, DoctorGetDto, DoctorCreateDto, DoctorUpdateDto>, IDoctorService
    {
        private readonly IEntityRepository<Address> _addressRepository;
        private readonly IEntityRepository<Person> _personRepository;
        private readonly IEntityRepository<Employee> _employeeRepository;

        public DoctorManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _addressRepository = unitOfWork.GenerateRepository<Address>();
            _personRepository = unitOfWork.GenerateRepository<Person>();
            _employeeRepository = unitOfWork.GenerateRepository<Employee>();
            BeginTransactionFlag = false;
            RollbackTransactionFlag = true;
            CommitTransactionFlag = true;
        }

        protected override async Task<DoctorGetDto> ConvertToDtoForGetAsync(Doctor input)
        {
            var doctor = await UnitOfWork.DoctorRepository.GetWithInclude(input.Id);
            return Mapper.Map<Doctor, DoctorGetDto>(doctor);
        }

        public override async Task<IDataResult<DoctorGetDto>> AddAsync(DoctorCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var address = Mapper.Map<AddressDto, Address>(input.Address);
            var person = Mapper.Map<DoctorCreateDto, Person>(input);
            var employee = Mapper.Map<DoctorCreateDto, Employee>(input);

            await UnitOfWork.BeginTransactionAsync();
            
            try
            {
                await _addressRepository.AddAsync(address);
                await _personRepository.AddAsync(person);

                employee.AddressId = address.Id;
                employee.PersonId = person.Id;

                await _employeeRepository.AddAsync(employee);

                return await base.AddAsync(input, new Dictionary<string, object>
                {
                    {"EmployeeId", employee.Id}
                });
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }

        public override async Task<IDataResult<DoctorGetDto>> UpdateAsync(Guid id, DoctorUpdateDto input, IDictionary<string, object> extraProperties = null)
        {
            var doctor = await UnitOfWork.DoctorRepository.GetWithInclude(id);

            if (doctor == null)
            {
                return new ErrorDataResult<DoctorGetDto>($"'{id}' id'li Doctor entity'si bulunamadı.");
            }

            var employeeId = doctor.EmployeeId;
            var personId = doctor.Employee.PersonId;
            var addressId = doctor.Employee.AddressId;
            var identityNumber = doctor.Employee.Person.IdentityNumber;
            
            var address = Mapper.Map<AddressDto, Address>(input.Address);
            address.Id = addressId;
            
            var person = Mapper.Map<DoctorUpdateDto, Person>(input);
            person.Id = personId;
            person.IdentityNumber = identityNumber;

            await UnitOfWork.BeginTransactionAsync();
            
            try
            {
                await _addressRepository.UpdateAsync(address);
                await _personRepository.UpdateAsync(person);

                return await base.UpdateAsync(id, input, new Dictionary<string, object>
                {
                    {"EmployeeId", employeeId}
                });
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }

        public override async Task<IResult> DeleteByIdAsync(Guid id)
        {
            var doctor = await UnitOfWork.DoctorRepository.GetWithInclude(id);

            if (doctor == null)
            {
                return new ErrorDataResult<DoctorGetDto>($"'{id}' id'li Doctor entity'si bulunamadı.");
            }

            var address = doctor.Employee.Address;
            var person = doctor.Employee.Person;
            
            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await _addressRepository.DeleteAsync(address);
                await _personRepository.DeleteAsync(person);
                await UnitOfWork.CommitTransactionAsync();
                return new SuccessResult($"'{id}' id'li Doctor entitysi silindi.");
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }
    }
}