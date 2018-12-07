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
    public class TreatmentsController : ControllerBase
    {
        private readonly DataContext _context;

        public TreatmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Treatments
        [HttpGet]
        public IEnumerable<TreatmentModel> GetTreatments()
        {
            var treatmenModels = _context.Treatments.Select(x =>
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
                });

            return treatmenModels;
        }

        // GET: api/Treatments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreatment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treatment = await _context.Treatments.FindAsync(id);
            var treatmentModel = new TreatmentModel()
            {
                TreatmentId = treatment.TreatmentId,
                StartDate = treatment.StartDate,
                Cures = treatment.Cures.Select(x =>
                    new CureModel()
                    {
                        CureId = x.CureId,
                        CareProductId = x.CareProductId,
                        DosageType = x.DosageType,
                        DosageValue = x.DosageValue
                    }).ToList(),
            };
            
            if (treatmentModel == null)
            {
                return NotFound();
            }

            return Ok(treatmentModel);
        }

        // PUT: api/Treatments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreatment([FromRoute] int id, [FromBody] TreatmentModel treatmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatmentModel.TreatmentId)
            {
                return BadRequest();
            }
            var treatment = await _context.Treatments.FindAsync(treatmentModel.TreatmentId);
            treatment.StartDate = treatmentModel.StartDate;
         
            _context.Entry(treatment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/Treatments
        [HttpPost]
        public async Task<IActionResult> PostTreatment([FromBody] ExtentendedTreatment treatmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treatment = new Treatment()
            {
                TreatmentId = treatmentModel.TreatmentId,
                DoctorId = treatmentModel.DoctorId,
                IllnessId = treatmentModel.IllnessId,
                Cures = treatmentModel.Cures.Select(
                    x => new Cure()
                    {
                        CureId = x.CureId,
                        CareProductId = x.CareProductId,
                        DosageType = x.DosageType,
                        DosageValue = x.DosageValue
                    }).ToList(),
                StartDate = treatmentModel.StartDate,
            };

            _context.Treatments.Add(treatment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTreatment", new { id = treatmentModel.TreatmentId }, treatment);
        }

        // DELETE: api/Treatments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }

            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync();

            return Ok(treatment);
        }

        private bool TreatmentExists(int id)
        {
            return _context.Treatments.Any(e => e.TreatmentId == id);
        }
    }
}