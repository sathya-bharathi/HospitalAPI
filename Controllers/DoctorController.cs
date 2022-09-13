using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly HMSDbContext db;
        public DoctorController(HMSDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<DoctorRegistration>> Login(DoctorRegistration a)
        {
            try
            {
                var Doctor = (from i in db.DoctorRegistrations
                             where i.DoctorId == a.DoctorId && i.Password == a.Password
                             select i).SingleOrDefault();
                if (Doctor == null)
                {
                    return BadRequest("Invalid Credential");
                }
                return Doctor;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Wrong Entry");
            }
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<IEnumerable<DoctorRegistration>>> GetDetails()
        {
            return await db.DoctorRegistrations.Include(x=>x.Specialization).ToListAsync();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatedetails(string id, DoctorRegistration doctor)
        {
            if (id != doctor.DoctorId)
            {
                return BadRequest();
            }

            db.Entry(doctor).State = EntityState.Modified;
            try
            {

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorRegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(doctor);
        }

        private bool DoctorRegistrationExists(string id)
        {
            return db.DoctorRegistrations.Include(x=>x.Specialization).Any(e => e.DoctorId == id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            var doctor = await db.DoctorRegistrations.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            db.DoctorRegistrations.Remove(doctor);
            await db.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("DoctorId")]

        public ActionResult<DoctorRegistration> GetDoctor(string DoctorId)

        {
            //var doctor = db.DoctorRegistrations.Include(x=>x.Specialization).Find(DoctorId);
            var doctor = (from i in db.DoctorRegistrations.Include(x => x.Specialization) where i.DoctorId == DoctorId select i).SingleOrDefault();
            if (doctor == null)
            {
                return NotFound();
            }
            return doctor;
        }
       
    }
}

