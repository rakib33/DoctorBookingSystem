using DoctorBooking.Domain.Entities;
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
    public class SlotRepository : ISlotRepository
    {
        private readonly ApplicationDbContext _db;

        public SlotRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddSlotAsync(int doctorId, DateTime startTime, DateTime endTime)
        {
            // Define start and end times in UTC
            var startUtc = startTime.ToUniversalTime();
            var endUtc = endTime.ToUniversalTime();

            // Check for overlapping slots
            bool overlaps = await _db.ScheduleSlots.AnyAsync(s =>
                    s.DoctorId == doctorId &&
                    s.StartTime < endUtc &&
                    s.EndTime > startUtc);

            if (overlaps) return false;

            var slot = new ScheduleSlot
            {
                DoctorId = doctorId,
                StartTime = startUtc,
                EndTime = endUtc,
                IsBooked = false,
                CreatedBy = "System", // Assuming system user for slot creation
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "System", // Assuming system user for slot update
                LastModifiedDate = DateTime.UtcNow
            };

            _db.ScheduleSlots.Add(slot);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<ScheduleSlot>> GetAllDoctorSlotsAsync()
        {
            //eager load doctor details for each slot
            return  _db.ScheduleSlots.AsNoTracking().Include(s => s.Doctor).OrderBy(s => s.StartTime);
        }

        public async Task<IEnumerable<ScheduleSlot>> GetDoctorSlotsAsync(int doctorId)
        {
            //eager load doctor details for each slot
            return await _db.ScheduleSlots.AsNoTracking().Include(s => s.Doctor)
                             .Where(s => s.DoctorId == doctorId)
                             .OrderBy(s => s.StartTime)
                             .ToListAsync();
        }
    }
}
