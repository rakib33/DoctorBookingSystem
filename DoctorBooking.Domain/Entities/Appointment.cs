using DoctorBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Domain.Entities
{
    public class Appointment : BaseEntity<Guid>
    {
        public DateTime AppointmentDate { get; set; }
        public int DoctorId { get; set; } // Foreign key to Doctor entity
        public Doctor Doctor { get; set; } // Navigation property to Doctor entity
        public Guid PatientId { get; set; } // Foreign key to Patient entity
        public Patient Patient { get; set; } // Navigation property to Patient entity
        public AppointmentStatus Status { get; set; } // Status of the appointment
        public string Notes { get; set; } // Additional notes for the appointment
        public Guid ScheduleSlotId { get; set; } // Foreign key to ScheduleSlot entity
        // Navigation property for schedule slot
        public ScheduleSlot ScheduleSlot { get; set; }

    }
}
