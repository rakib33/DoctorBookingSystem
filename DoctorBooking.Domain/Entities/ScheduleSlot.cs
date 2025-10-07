using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Domain.Entities
{
    public class ScheduleSlot : BaseEntity<Guid>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorId { get; set; } // Foreign key to Doctor entity
        public Doctor Doctor { get; set; } // Navigation property to Doctor entity
        public bool IsBooked { get; set; } // Indicates if the slot is booked
        public Appointment Appointment { get; set; }
    }
 
}
