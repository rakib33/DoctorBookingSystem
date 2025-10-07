using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorBooking.Application.DTOs
{
    public class ClinicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public String UpdatedBy { get; set; } 
        public bool IsActive { get; set; }
    }
}
