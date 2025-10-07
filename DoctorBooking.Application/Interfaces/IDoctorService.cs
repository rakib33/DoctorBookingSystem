using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task<Doctor?> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int id);
    }

}
