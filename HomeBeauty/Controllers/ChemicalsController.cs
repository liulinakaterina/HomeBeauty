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
    public class ChemicalsController : ControllerBase
    {
        private readonly DataContext _context;

        public ChemicalsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ChemicalModel> GetChemicals()
        {
            var chemicalModels = _context.Chemicals.Select(x =>
            new ChemicalModel()
            {
                ChemicalsId = x.ChemicalsId,
                Name = x.Name
            }).ToList();

            return chemicalModels;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChemicals([FromRoute] int id)
        {
            ChemicalModel chemicalModel = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chemicals = await _context.Chemicals.FindAsync(id);

            if (chemicals == null)
            {
                return NotFound();
            }
            else
            {
                chemicalModel = new ChemicalModel()
                {
                    ChemicalsId = chemicals.ChemicalsId,
                    Name = chemicals.Name
                };
            }

            return Ok(chemicalModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChemicals([FromRoute] int id, [FromBody] ChemicalModel chemicalModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chemicalModel.ChemicalsId)
            {
                return BadRequest();
            }

            var chemicals = await _context.Chemicals.FindAsync(id);
            chemicals.Name = chemicalModel.Name;

            _context.Entry(chemicals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChemicalsExists(id))
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

        [HttpPost]
        public async Task<IActionResult> PostChemicals([FromBody] ChemicalModel chemicalModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var chemicals = new Chemicals()
            {
                ChemicalsId = chemicalModel.ChemicalsId,
                Name = chemicalModel.Name
            };
            _context.Chemicals.Add(chemicals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChemicals", new { id = chemicalModel.ChemicalsId }, chemicals);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChemicals([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chemicals = await _context.Chemicals.FindAsync(id);
            if (chemicals == null)
            {
                return NotFound();
            }

            _context.Chemicals.Remove(chemicals);
            await _context.SaveChangesAsync();

            return Ok(chemicals);
        }

        private bool ChemicalsExists(int id)
        {
            return _context.Chemicals.Any(e => e.ChemicalsId == id);
        }
    }
}