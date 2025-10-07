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
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepo;

        public ClinicService(IClinicRepository clinicRepo)
        {
            _clinicRepo = clinicRepo;
        }

        public Task<List<Clinic>> GetAllAsync() => _clinicRepo.GetAllAsync();
        public Task<Clinic?> GetByIdAsync(int id) => _clinicRepo.GetByIdAsync(id);
        public Task<Clinic> AddAsync(Clinic clinic) => _clinicRepo.AddAsync(clinic);
        public Task<Clinic?> UpdateAsync(Clinic clinic) => _clinicRepo.UpdateAsync(clinic);
        public Task<bool> DeleteAsync(int id) => _clinicRepo.DeleteAsync(id);
    }

}
