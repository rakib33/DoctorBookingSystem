using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.DTOs
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public String UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
