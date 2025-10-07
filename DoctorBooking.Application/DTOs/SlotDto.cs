using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.DTOs
{
    public class SlotDto
    {
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public String UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
