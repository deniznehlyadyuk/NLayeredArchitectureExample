using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Utils;
using Core.Business.DTOs.Reservation;
using Core.Utils.Result;

namespace Business.Abstract
{
    public interface IReservationService : ICrudEntityService<ReservationGetDto, ReservationCreateDto, ReservationUpdateDto>
    {
        Task<IDataResult<ICollection<ReservationGetDto>>> GetListByDoctorId(Guid id);
    }
}