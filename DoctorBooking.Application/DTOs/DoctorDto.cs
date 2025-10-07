using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        //public string ClinicName { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public String UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
