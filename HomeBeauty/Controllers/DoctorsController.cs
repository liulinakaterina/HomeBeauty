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
    public class DoctorsController : ControllerBase
    {
        private readonly DataContext _context;

        public DoctorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("doctors")]
        public IEnumerable<DoctorModel> GetDoctors()
        {
            var doctors = _context.Doctors.Join(
                                _context.UsersInSystem,
                                doctorsData => doctorsData.UserId,
                                users => Convert.ToInt32(users.Id),
                                (d, u) => new
                                {
                                    DoctorId = d.DoctorId,
                                    Name = u.UserName,
                                    Role = u.Role,
                                    Birthday = u.Birthday,
                                    Country = u.Country,
                                    City = u.City,
                                    Address = u.Address,
                                    Qualification = d.Qualification,
                                    HospitalId = d.HospitalId
                                }).ToList().Select(x =>
                        new DoctorModel()
                        {
                            DoctorId = x.DoctorId,
                            Name = x.Name,
                            Role = x.Role,
                            Birthday = x.Birthday,
                            Country = x.Country,
                            City = x.City,
                            Address = x.Address,
                            Qualification = x.Qualification,
                            HospitalId = x.HospitalId
                        });
            return doctors;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctors([FromRoute] int id)
        {
            DoctorModel doctorModel;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _context.Doctors.FindAsync(id);
            var user = _context.UsersInSystem.Where(x => Convert.ToInt32(x.Id) == doctor.UserId).FirstOrDefault();

            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                var patientsCards = _context.Treatments.Where(x => x.DoctorId == id).Join(
                    _context.Illnesses,
                    t => t.IllnessId,
                    il => il.IllnessId,
                    (t, il) => new
                    {
                        user = il.UserId
                    });

                var patients = _context.UsersInSystem.Join(
                    patientsCards,
                    u => Convert.ToInt32(u.Id),
                    p => p.user,
                    (u, p) => new Patient()
                    {
                        UserId = Convert.ToInt32(u.Id),
                        Name = u.UserName
                    }).ToList();

                doctorModel = new DoctorModel()
                {
                    DoctorId = doctor.DoctorId,
                    UserId = Convert.ToInt32(user.Id),
                    Name = user.UserName,
                    Role = user.Role,
                    Birthday = user.Birthday,
                    Country = user.Country,
                    City = user.City,
                    Address = user.Address,
                    Qualification = doctor.Qualification,
                    HospitalId = doctor.HospitalId
                };
            }

            return Ok(doctor);
        }

        // GET: api/Doctors/Treatments/5
        [HttpGet("Treatments/{id}")]
        public async Task<IActionResult> GetTreatments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treatmentModels = _context.Treatments.Where(x => x.DoctorId == id)
                .Select(x =>
                new ExtentendedTreatment()
                {
                    TreatmentId = x.TreatmentId,
                    IllnessId = x.IllnessId,
                    DoctorId = x.DoctorId,
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
                treatmentModels = new List<ExtentendedTreatment>();
            }

            return Ok(treatmentModels);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor([FromRoute] int id, [FromBody] DoctorModel doctorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctorModel.DoctorId)
            {
                return BadRequest();
            }
            var doctor = await _context.Doctors.FindAsync(doctorModel.DoctorId);

            doctor.Qualification = doctorModel.Qualification;
            doctor.HospitalId = doctor.HospitalId;

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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
        public async Task<IActionResult> PostADoctor([FromBody] DoctorModel doctorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.UsersInSystem.Find(doctorModel.UserId);
            var doctor = new Doctor();
            doctor.DoctorId = doctorModel.DoctorId;
            doctor.UserId = Convert.ToInt32(user.Id);
            doctor.Qualification = doctorModel.Qualification;
            doctor.HospitalId = doctorModel.HospitalId;

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllergen", new { id = doctor.UserId }, doctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpGet("Patients/{id}")]
        public IEnumerable<Patient> GetPatients([FromRoute] int id)
        {
            var patientsCards = _context.Treatments.Where(x => x.DoctorId == id).Join(
                   _context.Illnesses,
                   t => t.IllnessId,
                   il => il.IllnessId,
                   (t, il) => new
                   {
                       user = il.UserId
                   });

            var patients = _context.UsersInSystem.Join(
                patientsCards,
                u => Convert.ToInt32(u.Id),
                p => p.user,
                (u, p) => new Patient()
                {
                    UserId = Convert.ToInt32(u.Id),
                    Name = u.UserName,
                    Allergens = u.Allergens.Select(x =>
                        new AllergenModel()
                        {
                            AllergenId = x.AllergenId,
                            Name = x.Name,
                            ChemicalId = x.ChemicalId
                        }).ToList()
                }).ToList();

            return patients;
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.UserId == id);
        }
    }
}