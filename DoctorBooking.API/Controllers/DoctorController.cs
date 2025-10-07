using AutoMapper;
using DoctorBooking.Application.DTOs;
using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorBooking.API.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(_mapper.Map<List<DoctorDto>>(await _service.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(_mapper.Map<DoctorDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorDto dto)
        {
            var created = await _service.AddAsync(_mapper.Map<Doctor>(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<DoctorDto>(created));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] DoctorDto dto)
        //{
        //    if (id != dto.Id) return BadRequest();
        //    var updated = await _service.UpdateAsync(_mapper.Map<Doctor>(dto));
        //    return updated == null ? NotFound() : Ok(_mapper.Map<DoctorDto>(updated));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var deleted = await _service.DeleteAsync(id);
        //    return deleted ? NoContent() : NotFound();
        //}
    }

}
