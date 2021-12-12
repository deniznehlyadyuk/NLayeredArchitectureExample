using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Business.DTOs.Reservation;
using Core.DataAccess;
using Core.Utils.Result;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ReservationManager : CrudEntityManager<Reservation, ReservationGetDto, ReservationCreateDto, ReservationUpdateDto>, IReservationService
    {
        private enum ResDateStatus
        {
            Ok,
            LessThan15Min,
            BusyForDateAndHour,
            BusyForDate
        }
        
        private readonly IEntityRepository<Person> _personRepository;
        
        public ReservationManager(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _personRepository = unitOfWork.GenerateRepository<Person>();
            BeginTransactionFlag = false;
            CommitTransactionFlag = true;
            RollbackTransactionFlag = true;
        }

        protected override async Task<ReservationGetDto> ConvertToDtoForGetAsync(Reservation input)
        {
            var reservation = await UnitOfWork.ReservationRepository.GetWithInclude(input.Id);
            return Mapper.Map<Reservation, ReservationGetDto>(reservation);
        }

        public override async Task<IDataResult<ReservationGetDto>> AddAsync(ReservationCreateDto input, IDictionary<string, object> extraProperties = null)
        {
            var person = Mapper.Map<ReservationCreateDto, Person>(input);
            var patient = await UnitOfWork.PatientRepository.GetByIdentityNumber(input.PatientInfo.IdentityNumber);

            await UnitOfWork.BeginTransactionAsync();

            try
            {
                if (patient == null)
                {
                    await _personRepository.AddAsync(person);
                    patient = new Patient { PersonId = person.Id };
                    await UnitOfWork.PatientRepository.AddAsync(patient);
                }
                
                var result = await CanBeReservationCreateOrUpdate(input.DoctorId, patient.Id, input.ResDate);

                if (!result.Success)
                {
                    await UnitOfWork.RollbackTransactionAsync();
                    return new ErrorDataResult<ReservationGetDto>(result.Message);
                }
                
                return await base.AddAsync(input, new Dictionary<string, object>
                {
                    {"PatientId", patient.Id},
                    {"IsCanceled", false}
                });
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }

        public override async Task<IDataResult<ReservationGetDto>> UpdateAsync(Guid id, ReservationUpdateDto input, IDictionary<string, object> extraProperties = null)
        {
            var reservation = await UnitOfWork.ReservationRepository.GetWithInclude(id);

            if (reservation == null)
            {
                return new ErrorDataResult<ReservationGetDto>($"'{id}' id'li Reservation entity'si bulunamadı.");
            }

            var patientId = reservation.PatientId;
            var patientPersonId = reservation.Patient.PersonId;
            var patientIdentityNumber = reservation.Patient.Person.IdentityNumber;

            var person = Mapper.Map<ReservationUpdateDto, Person>(input);
            person.Id = patientPersonId;
            person.IdentityNumber = patientIdentityNumber;
            
            var result = await CanBeReservationCreateOrUpdate(input.DoctorId, patientId, input.ResDate);

            if (!result.Success)
            {
                return new ErrorDataResult<ReservationGetDto>(result.Message);
            }
            
            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await _personRepository.UpdateAsync(person);
                return await base.UpdateAsync(id, input, new Dictionary<string, object>
                {
                    {"PatientId", patientId}
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
            return new ErrorResult("Rezervasyon bilgileri silinemez, IsCanceled alanı true edilerek güncellenebilir.");
        }

        private ResDateStatus? ResDateControl(ICollection<Reservation> reservations, DateTime resDate)
        {
            var isDoctorBusyForDate = reservations.Any(x => x.ResDate == resDate);

            if (isDoctorBusyForDate)
            {
                return ResDateStatus.BusyForDateAndHour;
            }

            isDoctorBusyForDate = reservations.Count(x => x.ResDate.Date.CompareTo(resDate.Date) == 0) > 10;

            if (isDoctorBusyForDate)
            {
                return ResDateStatus.BusyForDate;
            }

            var prevResDate = reservations.OrderByDescending(x => x.ResDate).FirstOrDefault(x => x.ResDate.CompareTo(resDate) < 0)?.ResDate;

            if (prevResDate == null)
            {
                return ResDateStatus.Ok;
            }
            
            TimeSpan ts = resDate.Subtract((DateTime) prevResDate);

            if (ts.Minutes < 15)
            {
                return ResDateStatus.LessThan15Min;
            }

            return ResDateStatus.Ok;
        }
        
        private async Task<IResult> CanBeReservationCreateOrUpdate(Guid doctorId, Guid patientId, DateTime resDate)
        {
            var doctor = await UnitOfWork.DoctorRepository.GetWithInclude(doctorId);

            if (doctor == null)
            {
                return null;
            }

            var doctorReservations = doctor.Reservations.Where(x => x.IsCanceled == false).ToList();
            var resDateControl = ResDateControl(doctorReservations, resDate);

            switch (resDateControl)
            {
                case ResDateStatus.Ok:
                    break;
                case ResDateStatus.BusyForDateAndHour:
                    return new ErrorResult("Doktor'un girilen tarih & saat için rezervasyonu mevcut.");
                case ResDateStatus.BusyForDate:
                    return new ErrorResult("Doktora bir gün için 10 adetten fazla randevu tanımlanamaz.");
                case ResDateStatus.LessThan15Min:
                    return new ErrorResult("Girilen tarih & saat bilgisi, bir önceki randevudan minimum 15 dk sonra olmalıdır.");
            }

            var isPatientHaveReservationForDate = await UnitOfWork.ReservationRepository.GetListAsync(x => x.PatientId == patientId && x.ResDate.Date.CompareTo(resDate.Date) == 0);

            if (isPatientHaveReservationForDate.Any())
            {
                return new ErrorResult("Hasta için o güne tanımlanmış bir rezervasyon zaten mevcut.");
            }

            return new SuccessResult();
        }
    }
}