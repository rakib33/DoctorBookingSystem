using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using DoctorBooking.Infrastructure.Data;
using DoctorBooking.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Services
{
    public class SlotService : ISlotService 
    {
        private readonly ISlotRepository _repo;

        public SlotService(ISlotRepository repo) => _repo = repo;

       public Task<bool> AddSlotAsync(int doctorId, DateTime startTime, DateTime endTime) => 
            _repo.AddSlotAsync(doctorId, startTime, endTime);
       public  Task<IEnumerable<ScheduleSlot>> GetDoctorSlotsAsync(int doctorId) => 
            _repo.GetDoctorSlotsAsync(doctorId);

      public Task<IQueryable<ScheduleSlot>> GetAllDoctorSlotsAsync()=> 
            _repo.GetAllDoctorSlotsAsync();
    }

}
