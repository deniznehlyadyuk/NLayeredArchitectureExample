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
    public class ReservationManager : BaseManager<Reservation, ReservationGetDto>, IReservationService
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
        }

        protected override async Task<ReservationGetDto> ConvertToDtoForGetAsync(Reservation input)
        {
            var reservation = await UnitOfWork.ReservationRepository.GetWithInclude(input.Id);
            return Mapper.Map<Reservation, ReservationGetDto>(reservation);
        }

        public async Task<IDataResult<ReservationGetDto>> AddAsync(ReservationCreateDto input)
        {
            var person = Mapper.Map<ReservationCreateDto, Person>(input);
            var patient = await UnitOfWork.PatientRepository.GetByIdentityNumber(input.PatientInfo.IdentityNumber);
            var reservation = Mapper.Map<ReservationCreateDto, Reservation>(input);
            
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

                reservation.PatientId = patient.Id;
                reservation.IsCanceled = false;
                await BaseEntityRepository.AddAsync(reservation);

                await UnitOfWork.CommitTransactionAsync();
                
                return await GetByIdAsync(reservation.Id);
            }
            catch (DbUpdateException ex)
            {
                await UnitOfWork.RollbackTransactionAsync();
                return GenerateErrorMessage(ex);
            }
        }

        public async Task<IDataResult<ReservationGetDto>> UpdateAsync(Guid id, ReservationUpdateDto input)
        {
            var reservation = await UnitOfWork.ReservationRepository.GetWithInclude(id);

            if (reservation == null)
            {
                return new ErrorDataResult<ReservationGetDto>($"'{id}' id'li Reservation entity'si bulunamadı.");
            }

            var patientId = reservation.PatientId;
            reservation.Patient.Person = Mapper.Map(input, reservation.Patient.Person);
            reservation = Mapper.Map(input, reservation);

            var result = await CanBeReservationCreateOrUpdate(input.DoctorId, patientId, input.ResDate);

            if (!result.Success)
            {
                return new ErrorDataResult<ReservationGetDto>(result.Message);
            }
            
            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await _personRepository.UpdateAsync(reservation.Patient.Person);
                await BaseEntityRepository.UpdateAsync(reservation);
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
            return new ErrorResult("Rezervasyon bilgileri silinemez, IsCanceled alanı true edilerek güncellenebilir.");
        }

        public async Task<IDataResult<ICollection<ReservationGetDto>>> GetListByDoctorId(Guid id)
        {
            var reservations = await UnitOfWork.ReservationRepository.GetWithInclude(x => x.DoctorId == id);
            var reservationDtos = new List<ReservationGetDto>();
            
            foreach (var reservation in reservations)
            {
                var reservationDto = Mapper.Map<Reservation, ReservationGetDto>(reservation); 
                reservationDtos.Add(reservationDto);
            }

            return new SuccessDataResult<ICollection<ReservationGetDto>>(reservationDtos);
        }

        private ResDateStatus? ResDateControl(ICollection<Reservation> reservations, DateTime resDate, Guid patientId)
        {
            var isDoctorBusyForDate = reservations.Any(x => x.ResDate == resDate && x.PatientId != patientId);

            if (isDoctorBusyForDate)
            {
                return ResDateStatus.BusyForDateAndHour;
            }

            isDoctorBusyForDate = reservations.Count(x => x.ResDate.Date.CompareTo(resDate.Date) == 0 && x.IsCanceled == false && x.PatientId != patientId) > 3;

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
            var resDateControl = ResDateControl(doctorReservations, resDate, patientId);

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

            var isPatientHaveReservationForDate = await UnitOfWork.ReservationRepository
                .GetListAsync(x => x.PatientId == patientId && x.ResDate.Date.CompareTo(resDate.Date) == 0 && x.DoctorId != doctorId);

            if (isPatientHaveReservationForDate.Any())
            {
                return new ErrorResult("Hasta için o güne tanımlanmış bir rezervasyon zaten mevcut.");
            }

            return new SuccessResult();
        }
    }
}