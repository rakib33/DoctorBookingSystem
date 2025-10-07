using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<bool> BookSlotAsync(Guid slotId, Guid patientId,string notes);
        Task<IEnumerable<ScheduleSlot>> SearchAvailableSlots(int doctorId, DateTime date);
        Task<IEnumerable<Appointment>> GetUpcomingAppointments(DateTime fromUtc);
    }

}
