using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Interfaces
{
    public interface IClinicService
    {
        Task<List<Clinic>> GetAllAsync();
        Task<Clinic?> GetByIdAsync(int id);
        Task<Clinic> AddAsync(Clinic clinic);
        Task<Clinic?> UpdateAsync(Clinic clinic);
        Task<bool> DeleteAsync(int id);
    }

}
