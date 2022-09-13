using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentBookingController : ControllerBase
    {
        private readonly HMSDbContext db;
        public AppointmentBookingController(HMSDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<IEnumerable<AppointmentBooking>>> GetDetails()
        {
            return await db.AppointmentBookings.Include(x => x.Doctor)
                .Include(x=>x.Patient).ToListAsync();
        }
        [HttpGet]
        [Route("DoctorId")]
        public ActionResult<List<AppointmentBooking>> GetAppointmentDetails(string DoctorId)
        {
            var appointment = (from i in db.AppointmentBookings where i.DoctorId == DoctorId select i).Include(x => x.Doctor)
                .Include(x => x.Patient).ToList();
            return appointment;
        }

        [HttpGet]
        [Route("PatientId")]
        public ActionResult<List<AppointmentBooking>> AppointmentDetails(string PatientId)
        {
            var appointment = (from i in db.AppointmentBookings where i.PatientId == PatientId select i).Include(x => x.Doctor)
                .Include(x => x.Patient).ToList();
            return appointment;
        }

    }
}
