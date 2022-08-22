using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly HMSDbContext db;
        public SpecializationController (HMSDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Specialization")]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        {
            return await db.Specializations.ToListAsync();
        }

    }
}
