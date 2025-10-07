using DoctorBooking.API.Utils;
using DoctorBooking.Application.Interfaces;
using DoctorBooking.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {

        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSlot([FromBody] SlotRequest request)
        {
            var success = await _slotService.AddSlotAsync(request.DoctorId, request.StartTime, request.EndTime);
            if (success)
                return Ok("Slot added successfully");
            else
                return Conflict("Slot overlaps with an existing one");
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorSlots(int doctorId)
        {
            var slots = await _slotService.GetDoctorSlotsAsync(doctorId);
            return Ok(slots);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slots = await _slotService.GetAllDoctorSlotsAsync();
            return Ok(slots);
        }
    }
}
