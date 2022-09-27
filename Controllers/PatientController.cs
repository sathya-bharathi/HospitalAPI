using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Repository;
using HospitalAPI.Token;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient db;
        public PatientController(IPatient db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult<PatientRegistration>> Registration(PatientRegistration patient)
        {
            db.Registration(patient);
            return patient;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<PatientToken>> Login(PatientRegistration Patient)
        {

            var ans = await db.Login(Patient);
            if (ans == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(ans);
            }
        }

        [HttpPost]
        [Route("BookAppointment")]
        public ActionResult<AppointmentBooking> AppointmentBook(AppointmentBooking appointment)
        {
            db.BookAppointment(appointment);
            return appointment;
        }

        [HttpGet]
        [Route("DoctorId")]

        public async Task<ActionResult<DoctorRegistration>> GetDoctordetail(string DoctorId)

        {
            return await db.GetDoctordetail(DoctorId);
        }
        [HttpGet]
        [Route("PatientId")]

        public async Task<ActionResult<PatientRegistration>> GetPatientdetail(string PatientId)

        {
            return await db.GetPatientdetail(PatientId);
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<List<PatientRegistration>>> Getdetails()
        {
            return await db.GetDetails();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatedetails(PatientRegistration patient)
        {
            await db.Updatedetails(patient);
            return Ok(patient);
        }
    }
}

       

    
