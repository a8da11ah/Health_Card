using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationController : ControllerBase
    {
        private readonly  IServiceBase<Vaccination,VaccinationFilter> _vaccinationService;

        public VaccinationController( IServiceBase<Vaccination,VaccinationFilter> vaccinationService)
        {
            _vaccinationService = vaccinationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccination>>> GetAll([FromQuery] VaccinationFilter filter)
        {

                var vaccinations = await _vaccinationService.GetAllAsync(filter);
                return Ok(vaccinations);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccination>> GetById(int id)
        {
            var vaccination = await _vaccinationService.GetByIdAsync(id);
            if (vaccination == null) return NotFound();
            return Ok(vaccination);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Vaccination vaccination)
        {
            await _vaccinationService.CreateAsync(vaccination);
            return CreatedAtAction(nameof(GetById), new { id = vaccination.VaccinationID }, vaccination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vaccination vaccination)
        { 
            if (id != vaccination.VaccinationID)
                return BadRequest("ID mismatch");
            
            var existing = await _vaccinationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _vaccinationService.UpdateAsync(vaccination);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _vaccinationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _vaccinationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
