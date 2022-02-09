using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Teacher;
using Core.DataAccess;
using Core.Utils.Results;
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
        
        public override async Task<IDataResult<TeacherGetDto>> AddAsync(TeacherCreateDto input)
        {
            var person = Mapper.Map<TeacherCreateDto, Person>(input);
            var teacher = Mapper.Map<TeacherCreateDto, Teacher>(input);

            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await _personRepository.AddAsync(person);
                teacher.PersonId = person.Id;

                await BaseEntityRepository.AddAsync(teacher);

                await UnitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(teacher.Id);
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<TeacherGetDto>(ex.Message);
            }
        }

        public override async Task<IDataResult<TeacherGetDto>> UpdateAsync(Guid id, TeacherUpdateDto input)
        {
            var teacher = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Person);

            if (teacher == null)
            {
                return new ErrorDataResult<TeacherGetDto>($"'{id}' id'li Teacher entitysi bulunamadı.");
            }

            if (teacher.Person == null)
            {
                return new ErrorDataResult<TeacherGetDto>($"'{teacher.PersonId}' id'li Person entitysi bulunamadı.");
            }
            
            teacher.Person = Mapper.Map(input, teacher.Person);

            await _personRepository.UpdateAsync(teacher.Person);

            return await GetByIdAsync(id);
        }
        public override async Task<IResult> DeleteByIdAsync(Guid id)
        {
            var teacher = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Person);
            
            if (teacher == null)
            {
                return new ErrorDataResult<TeacherGetDto>($"'{id}' id'li Teacher entitysi bulunamadı.");
            }

            if (teacher.Person == null)
            {
                return new ErrorDataResult<TeacherGetDto>($"'{teacher.PersonId}' id'li Person entitysi bulunamadı.");
            }
            
            await _personRepository.DeleteAsync(teacher.Person);

            return new SuccessResult($"'{id}' id'li Teacher silindi.");
        }
        
        public override async Task<IDataResult<TeacherGetDto>> GetByIdAsync(Guid id)
        {
            var teacher = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id, x => x.Person,x=>x.Lesson);

            if (teacher == null)
            {
                return new ErrorDataResult<TeacherGetDto>($"'{id}' id'li Teacher entitysi bulunamadı.");
            }
            
            var teacherDto = Mapper.Map<Teacher, TeacherGetDto>(teacher);
            return new SuccessDataResult<TeacherGetDto>(teacherDto);
        }

        public override async Task<IDataResult<ICollection<TeacherGetDto>>> GetAllAsync()
        {
            var teachers = await BaseEntityRepository.GetListWithIncludeAsync(null, x => x.Person, x => x.Lesson);
            var teacherDtos = Mapper.Map<List<Teacher>, List<TeacherGetDto>>(teachers.ToList());
            return new SuccessDataResult<ICollection<TeacherGetDto>>(teacherDtos);
        }
    } 
}
