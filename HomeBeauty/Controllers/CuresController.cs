using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeBeauty.Data;
using HomeBeauty.Entities;
using HomeBeauty.Models;

namespace HomeBeauty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuresController : ControllerBase
    {
        private readonly DataContext _context;

        public CuresController(DataContext context)
        {
            _context = context;
        }

        // PUT: api/Cures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCure([FromRoute] int id, [FromBody] CureModel cureModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cureModel.CureId)
            {
                return BadRequest();
            }

            var cure = await _context.Cures.FindAsync(cureModel.CureId);
            cure.CareProductId = cureModel.CareProductId;
            cure.DosageType = cureModel.DosageType;
            cure.DosageValue = cureModel.DosageValue;

            _context.Entry(cure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CureExists(id))
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

        // POST: api/Cures
        [HttpPost]
        public async Task<IActionResult> PostCure([FromBody] CureModel cureModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cure = new Cure()
            {
                CureId = cureModel.CureId,
                CareProductId = cureModel.CareProductId,
                DosageType = cureModel.DosageType,
                DosageValue = cureModel.DosageValue
            };

            _context.Cures.Add(cure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCure", new { id = cureModel.CureId }, cure);
        }

        // DELETE: api/Cures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCure([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cure = await _context.Cures.FindAsync(id);
            if (cure == null)
            {
                return NotFound();
            }

            _context.Cures.Remove(cure);
            await _context.SaveChangesAsync();

            return Ok(cure);
        }

        private bool CureExists(int id)
        {
            return _context.Cures.Any(e => e.CureId == id);
        }
    }
}