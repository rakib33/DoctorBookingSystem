using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Domain.Entities
{
    public class Clinic : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Website { get; set; }
        // Navigation properties
        public virtual ICollection<Doctor> Doctors { get; set; }
      
    }
}
