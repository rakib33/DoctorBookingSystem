using DoctorBooking.Domain.Entities;
using DoctorBooking.Infrastructure.Data;
using DoctorBooking.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorBooking.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Doctor>> GetAllAsync() =>
            await _context.Doctors.Include(d => d.Clinic).ToListAsync();

        public Task<Doctor?> GetByIdAsync(int id) =>
            _context.Doctors.Include(d => d.Clinic).FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
            var existing = await _context.Doctors.FindAsync(doctor.Id);
            if (existing == null) return null;

            existing.Name = doctor.Name;
            existing.Specialization = doctor.Specialization;
            existing.ClinicId = doctor.ClinicId;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
