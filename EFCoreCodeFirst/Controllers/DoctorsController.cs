using EFCoreCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly MainDbContext _context;

        public DoctorsController(MainDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("{idDoctor}")]
        public async Task<IActionResult> GetDoctorDetails(int idDoctor)
        {
            try
            {
                var doctor = await _context.Doctors.FindAsync(idDoctor);

                if (doctor == null)
                {
                    return StatusCode(404, "No doctor with such id was found");
                }

                var returnInfo = new
                {
                    doctor.IdDoctor,
                    doctor.FirstName,
                    doctor.LastName,
                    doctor.Email,
                    Prescriptions = doctor.Prescriptions.Select(p => new
                    {
                        p.IdPrescription,
                        p.Date,
                        p.DueDate,
                        p.IdDoctor,
                        p.IdPatient
                    })
                };

                return Ok(returnInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while processing the request");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDTO doctorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                
                // I decided to check uniqueness of the e-mail, despite it not being defined as a unique attribute I think it makes sense for it to not repeat
                var doesEmailExist = await _context.Doctors.AnyAsync(d => d.Email == doctorDto.Email);

                if (doesEmailExist)
                {
                    return StatusCode(409, "A doctor with such an email already exists in the database");
                }

                var newDoctor = new Doctor
                {
                    FirstName = doctorDto.FirstName,
                    LastName = doctorDto.LastName,
                    Email = doctorDto.Email
                };

                await _context.Doctors.AddAsync(newDoctor);

                await _context.SaveChangesAsync();

                return Ok(newDoctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        [HttpPut("{idDoctor}")]
        public async Task<IActionResult> UpdateDoctor(int idDoctor, [FromBody] DoctorDTO doctorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var doctor = await _context.Doctors.FindAsync(idDoctor);

                if (doctor == null)
                {
                    return NotFound();
                }

                // I decided to check uniqueness of the e-mail, despite it not being defined as a unique attribute I think it makes sense for it to not repeat
                var doesEmailExist = await _context.Doctors.AnyAsync(d => d.Email == doctorDto.Email && d.IdDoctor != idDoctor);

                if (doesEmailExist)
                {
                    return StatusCode(409, "A doctor with such an email already exists in the database");
                }

                doctor.FirstName = doctorDto.FirstName;
                doctor.LastName = doctorDto.LastName;
                doctor.Email = doctorDto.Email;

                await _context.SaveChangesAsync();

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        [HttpDelete("{idDoctor}")]
        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            try
            {
                var toDelete = await _context.Doctors.FindAsync(idDoctor);

                if (toDelete == null)
                {
                    return StatusCode(404, "Doctor not found in the system");
                }

                _context.Doctors.Remove(toDelete);

                await _context.SaveChangesAsync();

                return Ok("Doctor with id " + idDoctor + " successfully removed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
    }
}