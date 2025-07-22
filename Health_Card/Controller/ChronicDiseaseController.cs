
using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChronicDiseaseController : ControllerBase
    {
        private readonly IChronicDiseaseService _chronicDiseaseService;

        public ChronicDiseaseController(IChronicDiseaseService chronicDiseaseService)
        {
            _chronicDiseaseService = chronicDiseaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChronicDisease>>> GetAll()
        {
            var chronicDiseases = await _chronicDiseaseService.GetAllAsync();
            return Ok(chronicDiseases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChronicDisease>> GetById(int id)
        {
            var chronicDisease = await _chronicDiseaseService.GetByIdAsync(id);
            if (chronicDisease == null) return NotFound();
            return Ok(chronicDisease);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ChronicDisease chronicDisease)
        {
            await _chronicDiseaseService.CreateAsync(chronicDisease);
            return CreatedAtAction(nameof(GetById), new { id = chronicDisease.ChronicDiseaseID }, chronicDisease);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChronicDisease chronicDisease)
        { 
            if (id != chronicDisease.ChronicDiseaseID)
                return BadRequest("ID mismatch");
            
            var existing = await _chronicDiseaseService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _chronicDiseaseService.UpdateAsync(chronicDisease);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _chronicDiseaseService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _chronicDiseaseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
