using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeBeauty.Data;
using HomeBeauty.Entities;

namespace HomeBeauty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalCardsController : ControllerBase
    {
        private readonly DataContext _context;

        public MedicalCardsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/MedicalCards
        [HttpGet("MedicalCards")]
        public IEnumerable<MedicalCard> GetMedicalCards()
        {
            return _context.MedicalCards;
        }

        // GET: api/MedicalCards/5
        [HttpGet("MedicalCards/{id}")]
        public async Task<IActionResult> GetMedicalCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicalCard = await _context.MedicalCards.FindAsync(id);

            if (medicalCard == null)
            {
                return NotFound();
            }

            return Ok(medicalCard);
        }

        // PUT: api/MedicalCards/5
        [HttpPut("MedicalCards/{id}")]
        public async Task<IActionResult> PutMedicalCard([FromRoute] int id, [FromBody] MedicalCard medicalCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicalCard.MedicalCardId)
            {
                return BadRequest();
            }

            _context.Entry(medicalCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MedicalCards
        [HttpPost]
        public async Task<IActionResult> PostMedicalCard([FromBody] MedicalCard medicalCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MedicalCards.Add(medicalCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalCard", new { id = medicalCard.MedicalCardId }, medicalCard);
        }

        // DELETE: api/MedicalCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicalCard = await _context.MedicalCards.FindAsync(id);
            if (medicalCard == null)
            {
                return NotFound();
            }

            _context.MedicalCards.Remove(medicalCard);
            await _context.SaveChangesAsync();

            return Ok(medicalCard);
        }

        private bool MedicalCardExists(int id)
        {
            return _context.MedicalCards.Any(e => e.MedicalCardId == id);
        }
    }
}