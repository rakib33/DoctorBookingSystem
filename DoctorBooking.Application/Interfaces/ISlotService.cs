using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Interfaces
{
    public interface ISlotService
    {
        Task<bool> AddSlotAsync(int doctorId, DateTime startTime, DateTime endTime);
        Task<IEnumerable<ScheduleSlot>> GetDoctorSlotsAsync(int doctorId);
        Task<IQueryable<ScheduleSlot>> GetAllDoctorSlotsAsync();
    }
}
