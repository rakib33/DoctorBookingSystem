using AutoMapper;
using DoctorBooking.Application.DTOs;
using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorBooking.API.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        private readonly IMapper _mapper;

        public PatientController(IPatientService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(_mapper.Map<List<PatientDto>>(await _service.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(_mapper.Map<PatientDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientDto dto)
        {
            var created = await _service.AddAsync(_mapper.Map<Patient>(dto));
            created.Id = new Guid();
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<PatientDto>(created));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] PatientDto dto)
        //{
        //    if (id != dto.Id) return BadRequest();
        //    var updated = await _service.UpdateAsync(_mapper.Map<Patient>(dto));
        //    return updated == null ? NotFound() : Ok(_mapper.Map<PatientDto>(updated));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var deleted = await _service.DeleteAsync(id);
        //    return deleted ? NoContent() : NotFound();
        //}
    }

}
