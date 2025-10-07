using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using DoctorBooking.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo) => _repo = repo;

        public Task<IEnumerable<ScheduleSlot>> SearchAvailableSlots(int doctorId, DateTime date) =>
            Task.FromResult(_repo.GetAvailableSlots(doctorId, date).AsEnumerable());

        public Task<bool> BookSlotAsync(Guid slotId, Guid patientId, string notes) =>
            _repo.BookSlotAsync(slotId, patientId,notes);

        public Task<IEnumerable<Appointment>> GetUpcomingAppointments(DateTime fromUtc) =>
            Task.FromResult(_repo.GetUpcomingAppointmentsAsync(fromUtc).Result.AsEnumerable());
    }

}
