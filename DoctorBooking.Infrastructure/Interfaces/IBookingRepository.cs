using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Infrastructure.Interfaces
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Attempts to book a slot by patient. Returns false if slot is already booked.
        /// </summary>
        Task<bool> BookSlotAsync(Guid slotId, Guid patientId,string notes);

        /// <summary>
        /// Returns all available slots for a doctor on a specific date.
        /// </summary>
        IQueryable<ScheduleSlot> GetAvailableSlots(int doctorId, DateTime date);

        /// <summary>
        /// Gets all upcoming confirmed appointments from a specific point in time.
        /// </summary>
        Task<List<Appointment>> GetUpcomingAppointmentsAsync(DateTime fromUtc);
    }
}
