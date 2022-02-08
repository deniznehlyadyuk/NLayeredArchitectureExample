using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Utils;
using Core.Business.DTOs.Group;
using Core.Utils.Results;

namespace Business.Abstract
{
    public interface IGroupService : ICrudEntityService<GroupGetDto,GroupCreateDto,GroupUpdateDto>
    {
        public Task<IDataResult<ICollection<GroupGetDto>>> GetStudentsForTeacher(Guid teacherId);
        public Task<IDataResult<ICollection<GroupGetDto>>>GetTeachersForStudent(Guid studentId);
    }
}