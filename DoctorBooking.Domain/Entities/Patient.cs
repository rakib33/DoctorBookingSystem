using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Domain.Entities
{
    public class Patient : BaseEntity<Guid>
    {
        [StringLength(200)]
        public string Name { get; set; }
       
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
        // Navigation property for appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
  }
