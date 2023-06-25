using EFCoreCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly MainDbContext _context;

        public PrescriptionsController(MainDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idPrescription}")]
        public async Task<IActionResult> GetPrescriptionDetails(int idPrescription)
        {
            try
            {
                var prescription = await _context.Prescriptions.FindAsync(idPrescription);

                if (prescription == null)
                {
                    return StatusCode(404, "No prescription with such id");
                }

                var returnInfo = new
                {
                    prescription.IdPrescription,
                    prescription.Date,
                    prescription.DueDate,
                    Patient = new
                    {
                        prescription.Patient.IdPatient,
                        prescription.Patient.FirstName,
                        prescription.Patient.LastName,
                        prescription.Patient.BirthDate
                    },
                    Doctor = new
                    {
                        prescription.Doctor.IdDoctor,
                        prescription.Doctor.FirstName,
                        prescription.Doctor.LastName,
                        prescription.Doctor.Email
                    },
                    Medicaments = prescription.PrescriptionMedicaments.Select(pm => new
                    {
                        pm.Medicament.IdMedicament,
                        pm.Medicament.Name,
                        pm.Medicament.Description,
                        pm.Medicament.Type
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
    }
}