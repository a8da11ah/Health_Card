using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.ServantChronicTreatment;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServantChronicTreatmentController : ControllerBase
    {
        private readonly IServantChronicTreatmentService _servantChronicTreatmentService;

        public ServantChronicTreatmentController(IServantChronicTreatmentService servantChronicTreatmentService)
        {
            _servantChronicTreatmentService = servantChronicTreatmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServantChronicTreatment>>> GetAll()
        {
            var servantChronicTreatments = await _servantChronicTreatmentService.GetAllAsync();
            return Ok(servantChronicTreatments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServantChronicTreatment>> GetById(int id)
        {
            var servantChronicTreatment = await _servantChronicTreatmentService.GetByIdAsync(id);
            if (servantChronicTreatment == null) return NotFound();
            return Ok(servantChronicTreatment);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ServantChronicTreatment servantChronicTreatment)
        {
            await _servantChronicTreatmentService.CreateAsync(servantChronicTreatment);
            return CreatedAtAction(nameof(GetById), new { id = servantChronicTreatment.TreatmentID }, servantChronicTreatment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServantChronicTreatment servantChronicTreatment)
        { 
            if (id != servantChronicTreatment.TreatmentID)
                return BadRequest("ID mismatch");
            
            var existing = await _servantChronicTreatmentService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _servantChronicTreatmentService.UpdateAsync(servantChronicTreatment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _servantChronicTreatmentService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _servantChronicTreatmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
