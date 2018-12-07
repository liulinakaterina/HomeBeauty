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
    public class WaterReceptionsController : ControllerBase
    {
        private readonly DataContext _context;

        public WaterReceptionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/WaterReceptions
        [HttpGet]
        public IEnumerable<WaterReceptionModel> GetWaterReceptions()
        {
            var waterReceptionModels = _context.WaterReceptions.Select(x =>
                new WaterReceptionModel()
                {
                    WaterReceptionId = x.WaterReceptionId,
                    DeviceId = x.DeviceId,
                    UserId = x.UserId,
                    Time = x.Time,
                    Information = x.Information
                }
            );
            return waterReceptionModels;
        }

        // GET: api/WaterReceptions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWaterReception([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var waterReception = await _context.WaterReceptions.FindAsync(id);

            if (waterReception == null)
            {
                return NotFound();
            }

            var waterReceptionModel = new WaterReceptionModel()
            {
                WaterReceptionId = waterReception.WaterReceptionId,
                DeviceId = waterReception.DeviceId,
                UserId = waterReception.UserId,
                Time = waterReception.Time,
                Information = waterReception.Information
            };

            return Ok(waterReceptionModel);
        }

        // PUT: api/WaterReceptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaterReception([FromRoute] int id, [FromBody] WaterReceptionModel waterReceptionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != waterReceptionModel.WaterReceptionId)
            {
                return BadRequest();
            }
            var waterReception = await _context.WaterReceptions.FindAsync(waterReceptionModel.WaterReceptionId);

            waterReception.Time = waterReceptionModel.Time;
            waterReception.Information = waterReception.Information;
            _context.Entry(waterReceptionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterReceptionExists(id))
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

        // POST: api/WaterReceptions
        [HttpPost]
        public async Task<IActionResult> PostWaterReception([FromBody] WaterReceptionModel waterReceptionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var waterReception = new WaterReception()
            {
                WaterReceptionId = waterReceptionModel.WaterReceptionId,
                UserId = waterReceptionModel.UserId,
                DeviceId = waterReceptionModel.DeviceId,
                Time = waterReceptionModel.Time,
                Information = waterReceptionModel.Information
            };

            _context.WaterReceptions.Add(waterReception);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWaterReception", new { id = waterReception.WaterReceptionId }, waterReception);
        }

        // DELETE: api/WaterReceptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaterReception([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var waterReception = await _context.WaterReceptions.FindAsync(id);
            if (waterReception == null)
            {
                return NotFound();
            }

            _context.WaterReceptions.Remove(waterReception);
            await _context.SaveChangesAsync();

            return Ok(waterReception);
        }

        private bool WaterReceptionExists(int id)
        {
            return _context.WaterReceptions.Any(e => e.WaterReceptionId == id);
        }
    }
}