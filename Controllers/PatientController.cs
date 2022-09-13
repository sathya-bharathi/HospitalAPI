using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HMSDbContext db;
        public PatientController(HMSDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult<PatientRegistration>> Registration(PatientRegistration a)
        {
            try
            {
                await db.PatientRegistrations.AddAsync(a);
                await db.SaveChangesAsync();
                return Ok(a);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Patient Record");
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<PatientRegistration>> Login(PatientRegistration a)
        {
            try
            {
                var patient = (from i in db.PatientRegistrations
                               where i.PatientId == a.PatientId && i.Password == a.Password
                               select i).SingleOrDefault();
                if (patient == null)
                {
                    return BadRequest("Invalid Credential");
                }
                return patient;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Wrong Entry");
            }
        }
        [HttpPost]
        [Route("BookAppointment")]
        public ActionResult<AppointmentBooking> BookAppointment(AppointmentBooking appointment)
        {
            db.Add(appointment);
            db.SaveChanges();
            return appointment;
        }

        [HttpGet]
        [Route("DoctorId")]
        
        public ActionResult<DoctorRegistration> GetDoctordetail(string DoctorId)

        {
            var doctor = db.DoctorRegistrations.Find(DoctorId);
            if(doctor==null)
            {
                return NotFound();
            }
            return doctor;
        }
        [HttpGet]
        [Route("PatientId")]

        public ActionResult<PatientRegistration> GetPatientdetail(string PatientId)

        {
            var patient = db.PatientRegistrations.Find(PatientId);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<IEnumerable<PatientRegistration>>> GetDetails()
        {
            return await db.PatientRegistrations.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatedetails(string id, PatientRegistration patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;
            try
            {

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientRegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(patient);
        }

        private bool PatientRegistrationExists(string id)
        {
            return db.PatientRegistrations.Any(e => e.PatientId == id);
        }

    }
}
