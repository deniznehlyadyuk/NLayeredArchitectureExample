using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Student;
using Core.DataAccess;
using Core.Utils.Results;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class StudentManager : CrudEntityManager<Student, StudentGetDto, StudentCreateDto, StudentUpdateDto>, IStudentService
    {
        private readonly IEntityRepository<Person> _personRepository;
        
        public StudentManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _personRepository = unitOfWork.GenerateRepository<Person>();
        }

        public override async Task<IDataResult<StudentGetDto>> AddAsync(StudentCreateDto input)
        {
            var person = Mapper.Map<StudentCreateDto, Person>(input);
            var student = Mapper.Map<StudentCreateDto, Student>(input);

            await UnitOfWork.BeginTransactionAsync();
            
            try
            {
                await _personRepository.AddAsync(person);
                student.PersonId = person.Id;

                await BaseEntityRepository.AddAsync(student);

                await UnitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(student.Id);
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<StudentGetDto>(ex.Message);
            }
        }

        public override async Task<IDataResult<StudentGetDto>> UpdateAsync(Guid id, StudentUpdateDto input)
        {
            var student = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Person);
            
            if (student == null)
            {
                return new ErrorDataResult<StudentGetDto>($"'{id}' id'li Student entitysi bulunamadı.");
            }
            
            if (student.Person == null)
            {
                return new ErrorDataResult<StudentGetDto>($"'{student.PersonId}' id'li Person entitysi bulunamadı.");
            }

            student.Person = Mapper.Map(input, student.Person);

            try
            {
                await _personRepository.UpdateAsync(student.Person);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<StudentGetDto>(ex.Message);
            }

            return await GetByIdAsync(id);
        }

        public override async Task<IResult> DeleteByIdAsync(Guid id)
        {
            var student = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Person);
            
            if (student == null)
            {
                return new ErrorDataResult<StudentGetDto>($"'{id}' id'li Student entitysi bulunamadı.");
            }
            
            if (student.Person == null)
            {
                return new ErrorDataResult<StudentGetDto>($"'{student.PersonId}' id'li Person entitysi bulunamadı.");
            }
            
            await _personRepository.DeleteAsync(student.Person);

            return new SuccessResult($"'{id}' id'li Student entitysi silindi.");
        }

        public override async Task<IDataResult<StudentGetDto>> GetByIdAsync(Guid id)
        {
            var student = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Person);

            if (student == null)
            {
                return new ErrorDataResult<StudentGetDto>($"'{id}' id'li Student entitysi bulunamadı.");
            }
            
            var studentDto = Mapper.Map<Student, StudentGetDto>(student);
            return new SuccessDataResult<StudentGetDto>(studentDto);
        }

        public override async Task<IDataResult<ICollection<StudentGetDto>>> GetAllAsync()
        {
            var students = await BaseEntityRepository.GetListWithIncludeAsync(null, x => x.Person);
            var studentDtos = Mapper.Map<List<Student>, List<StudentGetDto>>(students.ToList());
            return new SuccessDataResult<ICollection<StudentGetDto>>(studentDtos);
        }
    }
}