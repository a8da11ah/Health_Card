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
    public class ServantMedicalReviewController : ControllerBase
    {
        private readonly IServiceBase<ServantMedicalReview,ServantMedicalReviewFilter> _servantMedicalReviewService;

        public ServantMedicalReviewController(IServiceBase<ServantMedicalReview,ServantMedicalReviewFilter> servantMedicalReviewService)
        {
            _servantMedicalReviewService = servantMedicalReviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServantMedicalReview>>> GetAll([FromQuery] ServantMedicalReviewFilter filter)
        {

                var servantMedicalReviews = await _servantMedicalReviewService.GetAllAsync(filter);
                return Ok(servantMedicalReviews);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServantMedicalReview>> GetById(int id)
        {
            var servantMedicalReview = await _servantMedicalReviewService.GetByIdAsync(id);
            if (servantMedicalReview == null) return NotFound();
            return Ok(servantMedicalReview);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ServantMedicalReview servantMedicalReview)
        {
            await _servantMedicalReviewService.CreateAsync(servantMedicalReview);
            return CreatedAtAction(nameof(GetById), new { id = servantMedicalReview.ReviewID }, servantMedicalReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServantMedicalReview servantMedicalReview)
        { 
            if (id != servantMedicalReview.ReviewID)
                return BadRequest("ID mismatch");
            
            var existing = await _servantMedicalReviewService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _servantMedicalReviewService.UpdateAsync(servantMedicalReview);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _servantMedicalReviewService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _servantMedicalReviewService.DeleteAsync(id);
            return NoContent();
        }
    }
}
