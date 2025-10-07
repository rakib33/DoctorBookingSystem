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
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Clinic>> GetAllAsync()
        {
            return await _context.Clinics.ToListAsync();
        }

        public async Task<Clinic?> GetByIdAsync(int id)
        {
            return await _context.Clinics.FindAsync(id);
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();
            return clinic;
        }

        public async Task<Clinic?> UpdateAsync(Clinic clinic)
        {
            var existing = await _context.Clinics.FindAsync(clinic.Id);
            if (existing == null) return null;

            existing.Name = clinic.Name;
            existing.Address = clinic.Address;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null) return false;

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
