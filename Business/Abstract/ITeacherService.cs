using System.Threading.Tasks;
using Business.Utils;
using Core.Business.DTOs.Teacher;
using Core.Utils.Results;

namespace Business.Abstract
{
    public interface ITeacherService : ICrudEntityService<TeacherGetDto,TeacherCreateDto,TeacherUpdateDto>
    {
        Task<IDataResult<object>> GreatestTeacherForAverage();
        Task<IDataResult<object>> WorstTeacherForAverage();
    }
}