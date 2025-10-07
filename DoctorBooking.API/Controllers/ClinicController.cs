using AutoMapper;
using DoctorBooking.Application.DTOs;
using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        private readonly IMapper _mapper;
        public ClinicController(IClinicService clinicService, IMapper mapper)
        {
            _clinicService = clinicService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinics = await _clinicService.GetAllAsync();
            var result = _mapper.Map<List<Clinic>>(clinics);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 999)
                throw new Exception("Simulated failure");

            var clinic = await _clinicService.GetByIdAsync(id);
            return clinic == null ? NotFound() : Ok(_mapper.Map<ClinicDto>(clinic));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClinicDto clinicDto)
        {
            var entity = _mapper.Map<Clinic>(clinicDto);
            var created = await _clinicService.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ClinicDto>(created));

        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] ClinicDto clinicDto)
        //{
        //    if (id != clinicDto.Id)
        //        return BadRequest("ID mismatch");

        //    var updated = await _clinicService.UpdateAsync(_mapper.Map<Clinic>(clinicDto));
        //    return updated == null ? NotFound() : Ok(_mapper.Map<ClinicDto>(updated));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _clinicService.DeleteAsync(id);
        //    return result ? NoContent() : NotFound();
        //}
    }
}
