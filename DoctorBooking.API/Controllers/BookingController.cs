using DoctorBooking.API.Utils;
using DoctorBooking.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _svc;

        public BookingController(IBookingService svc) => _svc = svc;

        [HttpGet("slots")]
        public async Task<IActionResult> GetSlots(int doctorId, DateTime date)
        {
            var slots = await _svc.SearchAvailableSlots(doctorId, date);
            return Ok(slots);
        }

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] BookRequest req)
        {
            bool success = await _svc.BookSlotAsync(req.SlotId, req.PatientId,req.Notes);
            return success ? Ok("Booked successful") : Conflict("Slot already booked");
        }
    }
}
