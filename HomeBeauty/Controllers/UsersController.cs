using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeBeauty.Data;
using HomeBeauty.Entities;
using HomeBeauty.Models;
using System;

namespace HomeBeauty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            var users = _context.UsersInSystem.Select(x =>
                new UserModel()
                {
                    UserId = Convert.ToInt32(x.Id),
                    Name = x.UserName,
                    Role = x.Role,
                    Email = x.Email,
                    Birthday = x.Birthday,
                    Country = x.Country,
                    City = x.City,
                    Address = x.Address
                });
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestedUser = await _context.UsersInSystem.FindAsync(id);
            var user = new UserModel()
            {
                UserId = Convert.ToInt32(requestedUser.Id),
                Name = requestedUser.UserName,
                Role = requestedUser.Role,
                Email = requestedUser.Email,
                Birthday = requestedUser.Birthday,
                Country = requestedUser.Country,
                City = requestedUser.City,
                Address = requestedUser.Address
            };

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5
        [HttpGet("UserAllergens/{id}")]
        public async Task<IActionResult> GetUserAllergens([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var allergens = _context.Allergens.Where(x => x.UserId == id).
                Select(x => new AllergenModel()
                {
                    AllergenId = x.AllergenId,
                    Name = x.Name,
                    ChemicalId = x.ChemicalId
                }).ToList();

            if (allergens == null)
            {
                return NotFound();
            }

            return Ok(allergens);
        }

        // GET: api/Users/5
        [HttpGet("WaterReceptions/{id}")]
        public async Task<IActionResult> GetWaterReceptions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var waterReceptions = _context.WaterReceptions.Where(x => x.UserId == id).
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


        // GET: api/Users/5
        [HttpGet("UserIllnesses/{id}")]
        public async Task<IActionResult> GetUserIllnesses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var illnesses = _context.Illnesses.Where(x => x.UserId == id).
                Select(x => new IllnessModel()
                {
                    IllnessId = x.IllnessId,
                    Name = x.Name,
                    Symptoms = x.Symptoms,
                    IsCured = x.IsCured
                }).ToList();

            if (illnesses == null)
            {
                return NotFound();
            }

            return Ok(illnesses);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UserModel registeredUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registeredUser.UserId)
            {
                return BadRequest();
            }
            var user = await _context.UsersInSystem.FindAsync(id);
            user.Address = registeredUser.Address;
            user.Birthday = registeredUser.Birthday;
            user.City = registeredUser.City;
            user.Country = registeredUser.Country;
            user.Email = registeredUser.Email;
            user.UserName = registeredUser.Name;
            user.Role = registeredUser.Role;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserModel registeredUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                Id = registeredUser.UserId.ToString(),
                Address = registeredUser.Address,
                Birthday = registeredUser.Birthday,
                City = registeredUser.City,
                Country = registeredUser.Country,
                Email = registeredUser.Email,
                UserName = registeredUser.Name,
                Role = registeredUser.Role
            };
            
            _context.UsersInSystem.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = Convert.ToInt32(user.Id) }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.UsersInSystem.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UsersInSystem.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.UsersInSystem.Any(e => Convert.ToInt32(e.Id) == id);
        }
    }
}