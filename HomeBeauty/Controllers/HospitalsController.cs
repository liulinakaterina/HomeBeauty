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
    public class HospitalsController : ControllerBase
    {
        private readonly DataContext _context;

        public HospitalsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public IEnumerable<HospitalModel> GetHospitals()
        {
            var hospitalModels = _context.Hospitals.Select(x =>
            new HospitalModel()
            {
                HospitalId = x.HospitalId,
                Name = x.Name,
                Country = x.Country,
                City = x.City,
                Address = x.Address,
                Phone = x.Phone,
                Email = x.Email,
                Doctors = _context.Doctors.Where(d => d.HospitalId == x.HospitalId).Join(
                            _context.Users,
                            doctors => doctors.UserId,
                            users => users.UserId,
                            (d, u) => new
                            {
                                Name = u.Name,
                                Role = u.Role,
                                Birthday = u.Birthday,
                                Country = u.Country,
                                City = u.City,
                                Address = u.Address,
                                Qualification = d.Qualification,
                                HospitalId = d.HospitalId
                            }).ToList().
                            Select(doctor =>
                                new DoctorModel()
                                {
                                    Name = doctor.Name,
                                    Role = doctor.Role,
                                    Birthday = doctor.Birthday,
                                    Country = doctor.Country,
                                    City = doctor.City,
                                    Address = doctor.Address,
                                    Qualification = doctor.Qualification,
                                    HospitalId = doctor.HospitalId
                                }).ToList()
            });


            return hospitalModels;
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospital([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospital = await _context.Hospitals.FindAsync(id);

            HospitalModel hospitalModel;

            if (hospital == null)
            {
                return NotFound();
            }
            else
            {
                hospitalModel = new HospitalModel()
                {
                    HospitalId = hospital.HospitalId,
                    Name = hospital.Name,
                    Country = hospital.Country,
                    City = hospital.City,
                    Address = hospital.Address,
                    Phone = hospital.Phone,
                    Email = hospital.Email,
                    Doctors = _context.Doctors.Where(d => d.HospitalId == hospital.HospitalId).
                        Join(
                            _context.Users,
                            doctors => doctors.UserId,
                            users => users.UserId,
                            (d, u) => new
                            {
                                Name = u.Name,
                                Role = u.Role,
                                Birthday = u.Birthday,
                                Country = u.Country,
                                City = u.City,
                                Address = u.Address,
                                Qualification = d.Qualification,
                                HospitalId = d.HospitalId
                            }).
                            ToList().Select(doctor =>
                                new DoctorModel()
                                {
                                    Name = doctor.Name,
                                    Role = doctor.Role,
                                    Birthday = doctor.Birthday,
                                    Country = doctor.Country,
                                    City = doctor.City,
                                    Address = doctor.Address,
                                    Qualification = doctor.Qualification,
                                    HospitalId = doctor.HospitalId
                                }).ToList()
                };
            }

            return Ok(hospitalModel);
        }

        // PUT: api/Hospitals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital([FromRoute] int id, [FromBody] HospitalModel hospitalModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hospitalModel.HospitalId)
            {
                return BadRequest();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            hospital.Name = hospitalModel.Name;
            hospital.Country = hospital.Country;
            hospital.City = hospital.City;
            hospital.Country = hospital.Country;
            hospital.Address = hospital.Address;
            hospital.Email = hospital.Email;
            hospital.Phone = hospital.Phone;

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
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

        // POST: api/Hospitals
        [HttpPost]
        public async Task<IActionResult> PostHospital([FromBody] HospitalModel hospitalModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hospital = new Hospital()
            {
                HospitalId = hospitalModel.HospitalId,
                Name = hospitalModel.Name,
                Country = hospitalModel.Country,
                City = hospitalModel.City,
                Address = hospitalModel.Address,
                Phone = hospitalModel.Phone,
                Email = hospitalModel.Email
            };
            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospital", new { id = hospitalModel.HospitalId }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return Ok(hospital);
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.HospitalId == id);
        }
    }
}