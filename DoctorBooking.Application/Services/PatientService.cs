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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo) => _repo = repo;

        public Task<List<Patient>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Patient?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);
        public Task<Patient> AddAsync(Patient patient) => _repo.AddAsync(patient);
        public Task<Patient?> UpdateAsync(Patient patient) => _repo.UpdateAsync(patient);
        public Task<bool> DeleteAsync(Guid id) => _repo.DeleteAsync(id);
    }

}
