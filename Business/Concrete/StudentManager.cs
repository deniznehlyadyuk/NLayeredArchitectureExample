using System;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Student;
using Core.DataAccess;
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

        public override async Task<StudentGetDto> ConvertToDtoForGetAsync(Student input)
        {
            var person = await _personRepository.GetAsync(x => x.Id == input.PersonId);

            if (person == null)
            {
                return Mapper.Map<Student, StudentGetDto>(input);
            }

            var studentGetDto = Mapper.Map<Student, StudentGetDto>(input);
            return Mapper.Map(person, studentGetDto);
        }

        public override async Task<StudentGetDto> AddAsync(StudentCreateDto input)
        {
            var person = Mapper.Map<StudentCreateDto, Person>(input);
            var student = Mapper.Map<StudentCreateDto, Student>(input);

            await UnitOfWork.BeginTransactionAsync();
            
            try
            {
                await _personRepository.AddAsync(person);
                student.PersonId = person.Id;

                await BaseEntityRepository.AddAsync(student);

                var studentGetDto = Mapper.Map<StudentCreateDto, StudentGetDto>(input);
                studentGetDto.Id = student.Id;

                await UnitOfWork.CommitTransactionAsync();
                
                return studentGetDto;
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public override async Task<StudentGetDto> UpdateAsync(Guid id, StudentUpdateDto input)
        {
            var student = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (student == null)
            {
                return null;
            }

            var person = await _personRepository.GetAsync(x => x.Id == student.PersonId);

            if (person == null)
            {
                return null;
            }

            person = Mapper.Map(input, person);

            await _personRepository.UpdateAsync(person);

            return await GetByIdAsync(id);
        }
    }
}