namespace DoctorBooking.API.Utils
{
    public class SlotRequest
    {
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
