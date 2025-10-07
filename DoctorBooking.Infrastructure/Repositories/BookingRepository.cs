using DoctorBooking.Domain.Entities;
using DoctorBooking.Domain.Enums;
using DoctorBooking.Infrastructure.Data;
using DoctorBooking.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) => _db = db;

        /// <summary>
        /// performing multiple related operations (e.g., create appointment + update slot status).
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="patientId"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public async Task<bool> BookSlotAsync(Guid slotId, Guid patientId,string notes)
        {
            using var tx = await _db.Database.BeginTransactionAsync();

            var slot = await _db.ScheduleSlots
                .Where(s => s.Id == slotId && !s.IsBooked).FirstOrDefaultAsync();


            if (slot == null) return false;

            //Update the slot to mark it as booked
            slot.IsBooked = true;

            //add the appointment record
            _db.Appointments.Add(new Appointment
            {
                ScheduleSlotId = slotId,
                PatientId = patientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = patientId.ToString(),
                DoctorId = slot.DoctorId,
                AppointmentDate = slot.StartTime,
                Notes = notes,
                UpdatedBy = patientId.ToString(),
                LastModifiedDate = DateTime.UtcNow,
                Status = AppointmentStatus.Confirmed
            });

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }

        public IQueryable<ScheduleSlot> GetAvailableSlots(int doctorId, DateTime date) =>
            _db.ScheduleSlots.AsNoTracking().Include(s=>s.Doctor)
               .Where(s => s.DoctorId == doctorId && !s.IsBooked
                       && s.StartTime.Date == date.Date);

        public Task<List<Appointment>> GetUpcomingAppointmentsAsync(DateTime from) =>
            _db.Appointments.AsNoTracking().Include(a=>a.Doctor)
               .Include(a => a.Patient)
               .Include(a => a.ScheduleSlot)
               .Where(a => a.ScheduleSlot.StartTime >= from && a.Status == AppointmentStatus.Confirmed)
               .ToListAsync();
    }
}
