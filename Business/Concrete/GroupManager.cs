using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Group;
using Core.Utils.Results;
using DataAccess;
using Domain;

namespace Business.Concrete
{
    public class GroupManager : CrudEntityManager<Group,GroupGetDto,GroupCreateDto,GroupUpdateDto>, IGroupService
    {
        public GroupManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }

        public override async Task<IDataResult<GroupGetDto>> GetByIdAsync(Guid id)
        {
            var entity = await BaseEntityRepository.GetWithIncludeAsync(x => x.Id == id,x=>x.Student,x=>x.Student.Person,
                x=>x.Teacher,x=>x.Teacher.Person,x=>x.Lesson);

            if (entity == null)
            {
                return new ErrorDataResult<GroupGetDto>($"'{id}' id'li {typeof(Group).Name} entitysi bulunamadı.");
            }

            var entityDto = Mapper.Map<Group, GroupGetDto>(entity);
            
            return new SuccessDataResult<GroupGetDto>(entityDto);
        }

        public override async Task<IDataResult<ICollection<GroupGetDto>>> GetAllAsync()
        {
            var entities = await BaseEntityRepository.GetListWithIncludeAsync(null,x=>x.Student,x=>x.Student.Person,
                x=>x.Teacher,x=>x.Teacher.Person,x=>x.Lesson);
            var entityDtos = Mapper.Map<List<Group>, List<GroupGetDto>>(entities.ToList());
            return new SuccessDataResult<ICollection<GroupGetDto>>(entityDtos);
        }


        public async Task<IDataResult<ICollection<GroupGetDto>>> GetStudentsForTeacher(Guid teacherId)
        {
            var entities = await BaseEntityRepository.GetListWithIncludeAsync(x=>x.TeacherId==teacherId,x=>x.Student,x=>x.Student.Person,
                x=>x.Teacher,x=>x.Teacher.Person,x=>x.Lesson);
            var entityDtos = Mapper.Map<List<Group>, List<GroupGetDto>>(entities.ToList());
            return new SuccessDataResult<ICollection<GroupGetDto>>(entityDtos);
        }

        public  async Task<IDataResult<ICollection<GroupGetDto>>> GetTeachersForStudent(Guid studentId)
        {
            var entities = await BaseEntityRepository.GetListWithIncludeAsync(x=>x.StudentId==studentId,x=>x.Student,x=>x.Student.Person,
                x=>x.Teacher,x=>x.Teacher.Person,x=>x.Lesson);
            var entityDtos = Mapper.Map<List<Group>, List<GroupGetDto>>(entities.ToList());
            return new SuccessDataResult<ICollection<GroupGetDto>>(entityDtos);
        }
    }
}