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
    public class DevicesController : ControllerBase
    {
        private readonly DataContext _context;

        public DevicesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public IEnumerable<DeviceModel> GetDevices()
        {
            var deviceModels = _context.Devices.Select(x =>
                new DeviceModel()
                {
                    DeviceId = x.DeviceId,
                    ProductionDate = x.ProductionDate,
                    IMEI = x.IMEI
                }).ToList();

            return deviceModels;
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.FindAsync(id);
            var deviceModel = new DeviceModel()
            {
                DeviceId = device.DeviceId,
                ProductionDate = device.ProductionDate,
                IMEI = device.IMEI
            };

            if (deviceModel == null)
            {
                return NotFound();
            }

            return Ok(deviceModel);
        }

        // GET: api/Devices/5
        [HttpGet("WaterReceptions/{id}")]
        public async Task<IActionResult> GetWaterReceptions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var waterReceptions = _context.WaterReceptions.Where(x => x.DeviceId == id).
                Select(x =>
                    new WaterReceptionModel()
                    {
                        WaterReceptionId = x.WaterReceptionId,
                        DeviceId = x.DeviceId,
                        UserId = x.UserId,
                        Information = x.Information,
                        Time = x.Time
                    }
                ).ToList();

            if (waterReceptions == null)
            {
                waterReceptions = new List<WaterReceptionModel>();
            }

            return Ok(waterReceptions);
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] DeviceModel deviceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceModel.DeviceId)
            {
                return BadRequest();
            }
            var device = await _context.Devices.FindAsync(deviceModel.DeviceId);
            device.IMEI = deviceModel.IMEI;
            device.ProductionDate = deviceModel.ProductionDate;

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Devices
        [HttpPost]
        public async Task<IActionResult> PostDevice([FromBody] DeviceModel deviceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var device = new Device()
            {
                DeviceId = deviceModel.DeviceId,
                ProductionDate = deviceModel.ProductionDate,
                IMEI = deviceModel.IMEI
            };
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok(device);
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.DeviceId == id);
        }
    }
}