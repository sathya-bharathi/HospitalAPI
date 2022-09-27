using HospitalAPI.Models;
using HospitalAPI.Repository;
using HospitalAPI.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin db;
        public AdminController(IAdmin db)
        {
            this.db = db;
        }
      
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AdminToken>> Login(Admin admin)
        {
           var ans = await db.Login(admin);
           if(ans==null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(ans);
            }
        }
        [HttpPost]
        [Route("Registration")]
        public ActionResult<DoctorRegistration> Register(DoctorRegistration doctor)
        {
            db.Register(doctor);
            return doctor;
        }

    }
}



