using HospitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Repository;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentBookingController : ControllerBase
    {
        private readonly IAppointmentBooking db;
        public AppointmentBookingController(IAppointmentBooking db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<List<AppointmentBooking>>> GetDetails()
        {
            return await db.GetDetails();
        }
        [HttpGet]
        [Route("DoctorId")]
        public async Task<List<AppointmentBooking>> GetAppointmentDetails(string DoctorId)
        {
            return await db.GetAppointmentDetails(DoctorId);
        }

        [HttpGet]
        [Route("PatientId")]
        public async Task<List<AppointmentBooking>> AppointmentDetails(string PatientId)
        {
            return await db.AppointmentDetails(PatientId);
        }

    }
}
