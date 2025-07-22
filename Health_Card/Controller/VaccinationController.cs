using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.Vaccination;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationController : ControllerBase
    {
        private readonly IVaccinationService _vaccinationService;

        public VaccinationController(IVaccinationService vaccinationService)
        {
            _vaccinationService = vaccinationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccination>>> GetAll()
        {
            var vaccinations = await _vaccinationService.GetAllAsync();
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
