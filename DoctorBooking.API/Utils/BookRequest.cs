namespace DoctorBooking.API.Utils
{
    public class BookRequest
    {
        public Guid SlotId { get; set; }
        public Guid PatientId { get; set; }
        public string Notes { get; set; } // Optional notes for the appointment
    }
}
