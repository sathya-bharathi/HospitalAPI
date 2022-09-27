using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace HospitalAPI.Repository
{
    public class AppointmentBookingRepo : IAppointmentBooking
    {
        private readonly HMSDbContext db;
        public AppointmentBookingRepo(HMSDbContext db)
        {
            this.db = db;
        }
        public async Task<List<AppointmentBooking>> AppointmentDetails(string PatientId)
        {
            var appointment = (from i in db.AppointmentBookings where i.PatientId == PatientId select i).Include(x => x.Doctor)
                 .Include(x => x.Patient).ToList();
            return appointment;

        }

        public async Task<List<AppointmentBooking>> GetAppointmentDetails(string DoctorId)
        {
            var appointment = (from i in db.AppointmentBookings where i.DoctorId == DoctorId select i).Include(x => x.Doctor)
                .Include(x => x.Patient).ToList();
            return appointment;
        }

        public async Task<List<AppointmentBooking>> GetDetails()
        {
            return await db.AppointmentBookings.Include(x => x.Doctor)
               .Include(x => x.Patient).ToListAsync();
        }
    }
}
