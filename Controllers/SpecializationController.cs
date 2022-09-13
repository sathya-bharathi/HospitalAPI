using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpPost]
        public async Task<ActionResult<Specialization>> AddSpecialization(Specialization specialization)
        {
            db.Specializations.Add(specialization);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetSpecialization", new { id = specialization.SpecializationId }, specialization);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialization(int id, Specialization specialization)
        {
            if (id != specialization.SpecializationId)
            {
                return BadRequest();
            }

            db.Entry(specialization).State = EntityState.Modified;
            try
            {

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(specialization);
        }

        private bool SpecializationExists(int id)
        {
            return db.Specializations.Any(e => e.SpecializationId == id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await db.Specializations.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            db.Specializations.Remove(specialization);
            await db.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("SpecializationId")]

        public ActionResult<Specialization> GetSpecialization(int SpecializationId)

        {
            var specialization = db.Specializations.Find(SpecializationId);
            if (specialization == null)
            {
                return NotFound();
            }
            return specialization;
        }
    }
}
