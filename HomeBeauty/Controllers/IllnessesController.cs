using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeBeauty.Data;
using HomeBeauty.Entities;
using HomeBeauty.Models;

namespace HomeBeauty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IllnessesController : ControllerBase
    {
        private readonly DataContext _context;

        public IllnessesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Illnesses
        [HttpGet]
        public IEnumerable<IllnessModel> GetIllnesses()
        {
            var illnesses = _context.Illnesses.Select(x =>
                new IllnessModel()
                {
                    IllnessId = x.IllnessId,
                    Name = x.Name,
                    Symptoms = x.Symptoms,
                    IsCured = x.IsCured
                }
            );
            return illnesses;
        }

        // GET: api/Illnesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIllness([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var illness = await _context.Illnesses.FindAsync(id);
            var illnessModel = new IllnessModel()
            {
                IllnessId = illness.IllnessId,
                Name = illness.Name,
                Symptoms = illness.Symptoms,
                IsCured = illness.IsCured
            };

            if (illness == null)
            {
                return NotFound();
            }

            return Ok(illnessModel);
        }

        // GET: api/Illnesses/5
        [HttpGet("Treatments/{id}")]
        public async Task<IActionResult> GetTreatments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treatmentModels = _context.Treatments.Where(x => x.IllnessId == id)
                .Select(x =>
                new TreatmentModel()
                {
                    TreatmentId = x.TreatmentId,
                    StartDate = x.StartDate,
                    Cures = x.Cures.Select(c =>
                        new CureModel()
                        {
                            CureId = c.CureId,
                            CareProductId = c.CareProductId,
                            DosageType = c.DosageType,
                            DosageValue = c.DosageValue
                        }).ToList()
                }).ToList();

            if (treatmentModels == null)
            {
                treatmentModels = new List<TreatmentModel>();
            }

            return Ok(treatmentModels);
        }

        // PUT: api/Illnesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIllness([FromRoute] int id, [FromBody] IllnessModel illnessModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != illnessModel.IllnessId)
            {
                return BadRequest();
            }

            var illness = await _context.Illnesses.FindAsync(illnessModel.IllnessId);

            illness.IsCured = illnessModel.IsCured;
            illness.Name = illnessModel.Name;
            illness.Symptoms = illnessModel.Symptoms;

            _context.Entry(illnessModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllnessExists(id))
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

        // POST: api/Illnesses
        [HttpPost]
        public async Task<IActionResult> PostIllness([FromBody] IllnessModel illnessModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var illness = new Illness()
            {
                IllnessId = illnessModel.IllnessId,
                Name = illnessModel.Name,
                Symptoms = illnessModel.Symptoms,
                IsCured = illnessModel.IsCured
            };
            _context.Illnesses.Add(illness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIllness", new { id = illness.IllnessId }, illness);
        }

        // DELETE: api/Illnesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIllness([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var illness = await _context.Illnesses.FindAsync(id);
            if (illness == null)
            {
                return NotFound();
            }

            _context.Illnesses.Remove(illness);
            await _context.SaveChangesAsync();

            return Ok(illness);
        }

        private bool IllnessExists(int id)
        {
            return _context.Illnesses.Any(e => e.IllnessId == id);
        }
    }
}