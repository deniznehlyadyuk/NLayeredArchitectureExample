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
    public class DoctorManager : BaseManager<Doctor, DoctorGetDto>, IDoctorService
    {
        private readonly IEntityRepository<Address> _addressRepository;
        private readonly IEntityRepository<Person> _personRepository;
        private readonly IEntityRepository<Employee> _employeeRepository;

        public DoctorManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _addressRepository = unitOfWork.GenerateRepository<Address>();
            _personRepository = unitOfWork.GenerateRepository<Person>();
            _employeeRepository = unitOfWork.GenerateRepository<Employee>();
        }

        protected override async Task<DoctorGetDto> ConvertToDtoForGetAsync(Doctor input)
        {
            var doctor = await UnitOfWork.DoctorRepository.GetWithInclude(input.Id);
            return Mapper.Map<Doctor, DoctorGetDto>(doctor);
        }

        public async Task<IDataResult<DoctorGetDto>> AddAsync(DoctorCreateDto input)
        {
            var address = Mapper.Map<AddressDto, Address>(input.Address);
            var person = Mapper.Map<DoctorCreateDto, Person>(input);
            var employee = Mapper.Map<DoctorCreateDto, Employee>(input);
            var doctor = new Doctor();

            await UnitOfWork.BeginTransactionAsync();
            
            try
            {
                await _addressRepository.AddAsync(address);
                await _personRepository.AddAsync(person);

                employee.AddressId = address.Id;
                employee.PersonId = person.Id;

                await _employeeRepository.AddAsync(employee);

                doctor.EmployeeId = employee.Id;
                
                await BaseEntityRepository.AddAsync(doctor);

                await UnitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(doctor.Id);
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }

        public async Task<IDataResult<DoctorGetDto>> UpdateAsync(Guid id, DoctorUpdateDto input)
        {
            var doctor = await UnitOfWork.DoctorRepository.GetWithInclude(id);

            if (doctor == null)
            {
                return new ErrorDataResult<DoctorGetDto>($"'{id}' id'li Doctor entity'si bulunamadı.");
            }

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
                await UnitOfWork.CommitTransactionAsync();
                return await GetByIdAsync(id);
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }

        public async Task<IResult> DeleteByIdAsync(Guid id)
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