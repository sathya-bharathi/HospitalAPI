using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly HMSDbContext db;
        public AdminController(HMSDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Admin>> Login(Admin a)
        {
            try
            {
                var Admin = (from i in db.Admins
                             where i.AdminId == a.AdminId && i.Password == a.Password
                             select i).SingleOrDefault();
                if (Admin == null)
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
        [HttpPost]
        [Route("Registration")]
        public ActionResult <DoctorRegistration> Registration(DoctorRegistration doctor)
        {
            db.Add(doctor);
            db.SaveChanges();
            return doctor;
        }
        

    }
}

