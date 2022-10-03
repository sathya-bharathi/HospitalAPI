using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using HospitalAPI.Repository;
using HospitalAPI.Token;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor db;
        public DoctorController(IDoctor db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<DoctorToken>> Login(DoctorRegistration doctor)
        {
            var ans = await db.Login(doctor);
            if (ans == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(ans);
            }
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<List<DoctorRegistration>>> Getdetails()
        {
            return await db.GetDetails();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatedetails(DoctorRegistration doctor)
        {
            await db.UpdateDetails(doctor);
            return Ok(doctor);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            db.DeleteDoctor(id);
            return NoContent();
        }
        [HttpGet]
        [Route("DoctorId")]

        public async Task<ActionResult<DoctorRegistration>> GetDoctor(string id)
        {
            return await db.GetDetails(id);  
        }
      

    }
  

}

