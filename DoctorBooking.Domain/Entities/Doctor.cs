using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Specialization { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        public int ClinicId { get; set; } // Foreign key to Clinic entity
        public Clinic Clinic { get; set; } // Navigation property to Clinic entity
                
    } 
}
