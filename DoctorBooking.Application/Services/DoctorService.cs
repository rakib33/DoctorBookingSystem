using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using DoctorBooking.Infrastructure.Interfaces;


namespace DoctorBooking.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo) => _repo = repo;

        public Task<List<Doctor>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Doctor?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Doctor> AddAsync(Doctor doctor) => _repo.AddAsync(doctor);
        public Task<Doctor?> UpdateAsync(Doctor doctor) => _repo.UpdateAsync(doctor);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }

}
