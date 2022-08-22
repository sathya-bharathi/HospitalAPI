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
                return Accepted();
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
            return await db.DoctorRegistrations.ToListAsync();
        }
    }
}
