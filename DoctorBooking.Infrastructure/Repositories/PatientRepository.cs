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
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context) => _context = context;

        public Task<List<Patient>> GetAllAsync() => _context.Patients.ToListAsync();
        public Task<Patient?> GetByIdAsync(Guid id) => _context.Patients.FindAsync(id).AsTask();

        public async Task<Patient> AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> UpdateAsync(Patient patient)
        {
            var existing = await _context.Patients.FindAsync(patient.Id);
            if (existing == null) return null;

            existing.Name = patient.Name;
            existing.Email = patient.Email;
            existing.PhoneNumber = patient.PhoneNumber;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
