using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;
using Health_Card.Interface;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalReferralController : ControllerBase
    {
        private readonly IServiceBase<MedicalReferral, MedicalReferralFilter> _medicalReferralService;

        public MedicalReferralController(IServiceBase<MedicalReferral, MedicalReferralFilter> medicalReferralService)
        {
            _medicalReferralService = medicalReferralService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalReferral>>> GetAll([FromQuery] MedicalReferralFilter filter)
        {

                var medicalReferrals = await _medicalReferralService.GetAllAsync(filter);
                return Ok(medicalReferrals);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalReferral>> GetById(int id)
        {
            var medicalReferral = await _medicalReferralService.GetByIdAsync(id);
            if (medicalReferral == null) return NotFound();
            return Ok(medicalReferral);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MedicalReferral medicalReferral)
        {
            await _medicalReferralService.CreateAsync(medicalReferral);
            return CreatedAtAction(nameof(GetById), new { id = medicalReferral.ReferralID }, medicalReferral);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalReferral medicalReferral)
        { 
            if (id != medicalReferral.ReferralID)
                return BadRequest("ID mismatch");
            
            var existing = await _medicalReferralService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _medicalReferralService.UpdateAsync(medicalReferral);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _medicalReferralService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _medicalReferralService.DeleteAsync(id);
            return NoContent();
        }
    }
}
