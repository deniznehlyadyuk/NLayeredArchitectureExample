using System;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Teacher;
using Core.DataAccess;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class TeacherManager : CrudEntityManager<Teacher,TeacherGetDto,TeacherCreateDto,TeacherUpdateDto> , ITeacherService
    {
        private readonly IEntityRepository<Person> _personRepository;

        public TeacherManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _personRepository = unitOfWork.GenerateRepository<Person>();
        }

        public override async Task<TeacherGetDto> ConvertToDtoForGetAsync(Teacher input)
        {
            var person = await _personRepository.GetAsync(x => x.Id == input.PersonId);

            if (person == null)
            {
                return Mapper.Map<Teacher, TeacherGetDto>(input);
            }

            var teacherGetDto = Mapper.Map<Teacher, TeacherGetDto>(input);
            return Mapper.Map(person, teacherGetDto);
        }
        public override async Task<TeacherGetDto> AddAsync(TeacherCreateDto input)
        {
            var person = Mapper.Map<TeacherCreateDto, Person>(input);
            var teacher = Mapper.Map<TeacherCreateDto, Teacher>(input);

            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await _personRepository.AddAsync(person);
                teacher.PersonId = person.Id;

                await BaseEntityRepository.AddAsync(teacher);

                var studentGetDto = Mapper.Map<TeacherCreateDto, TeacherGetDto>(input);
                studentGetDto.Id = teacher.Id;

                await UnitOfWork.CommitTransactionAsync();

                return studentGetDto;
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public override async Task<TeacherGetDto> UpdateAsync(Guid id, TeacherUpdateDto input)
        {
            var teacher = await BaseEntityRepository.GetAsync(x => x.Id == id);

            if (teacher == null)
            {
                return null;
            }

            var person = await _personRepository.GetAsync(x => x.Id == teacher.PersonId);

            if (person == null)
            {
                return null;
            }

            person = Mapper.Map(input, person);

            await _personRepository.UpdateAsync(person);

            return await GetByIdAsync(id);
        }
        public override async Task DeleteByIdAsync(Guid id)
        {
            var teacher = await BaseEntityRepository.GetAsync(x => x.Id == id);
            var person = await _personRepository.GetAsync(x => x.Id == teacher.PersonId);

            await UnitOfWork.BeginTransactionAsync();
            if (teacher == null)
            {
                await UnitOfWork.RollbackTransactionAsync();
            }

            if (person == null)
            {
                await UnitOfWork.RollbackTransactionAsync();
            }
            
            await _personRepository.DeleteAsync(person);

            await UnitOfWork.CommitTransactionAsync();
        }
        
    } 
}
