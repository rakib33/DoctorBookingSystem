using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(Guid id);
        Task<Patient> AddAsync(Patient patient);
        Task<Patient?> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(Guid id);
    }
}
