using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Repository;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecialization db;
        public SpecializationController (ISpecialization db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Specialization")]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        {
            return await db.GetSpecializations();
        }
        [HttpPost]
        public async Task<ActionResult<Specialization>> AddSpecialization(Specialization specialization)
        {
           if (await db.AddSpecialization(specialization)==null)
            {
                return BadRequest();

            }
            else
            {
                return CreatedAtAction("GetSpecializations", new { id = specialization.SpecializationId }, specialization);
            }
        }
        [HttpPut("{id}")]
        public async Task<Specialization> UpdateSpecialization(int id, Specialization specialization)
        {
            await db.UpdateSpecialization(specialization);
            return specialization;
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            db.DeleteSpecialization(id);
            return NoContent();
        }
        [HttpGet]
        [Route("SpecializationId")]

        public async Task<Specialization> GetSpecialization(int SpecializationId)

        {
            return await db.GetSpecialization(SpecializationId);
        }
    }
}
