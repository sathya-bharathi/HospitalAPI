using HospitalAPI.Models;
using HospitalAPI.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalAPI.Repository
{
    public class PatientRepo : IPatient
    {
        private readonly HMSDbContext db;
        private readonly IConfiguration _configuration;
        public PatientRepo(HMSDbContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;
        }
        public async Task<AppointmentBooking> BookAppointment(AppointmentBooking appointment)
        {
            db.AppointmentBookings.Add(appointment);
            await db.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<PatientRegistration>> GetDetails()
        {
            return await db.PatientRegistrations.ToListAsync();

        }

        public async Task<DoctorRegistration> GetDoctordetail(string DoctorId)
        {
            return await db.DoctorRegistrations.FindAsync(DoctorId);

        }

        public async Task<PatientRegistration> GetPatientdetail(string PatientId)
        {
            return await db.PatientRegistrations.FindAsync(PatientId);
        }

        //public async Task<PatientRegistration> Login(PatientRegistration Patient)
        //{
        //    var patient = await(from i in db.PatientRegistrations where i.PatientId == Patient.PatientId && i.Password == Patient.Password select i).SingleOrDefaultAsync();
        //    try
        //    {
        //        if (patient != null)
        //        {
        //            return patient;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return null;
        //}
        public async Task<PatientToken> Login(PatientRegistration patient)

        {
            PatientToken ad = new PatientToken();
            var result = await (from i in db.PatientRegistrations where i.Password == patient.Password && i.PatientId == patient.PatientId select i).SingleOrDefaultAsync();
            ad.patient = result;
            if (result != null)
            {
                var authClaims = new List<Claim>
        { new Claim(ClaimTypes.SerialNumber, patient.PatientId), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };

                var token = GetToken(authClaims);
                string s = new JwtSecurityTokenHandler().WriteToken(token);
                ad.patientToken = s;
                ad.patient = result;
                return ad;



            }
            return null;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                  issuer: _configuration["JWT:ValidIssuer"],
                  audience: _configuration["JWT:ValidAudience"],
                  expires: DateTime.Now.AddMinutes(30),
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                  ); ;
            return token;
        }

        public bool PatientRegistrationExists(string id)
        {
            return (db.PatientRegistrations?.Any(e => e.PatientId == id)).GetValueOrDefault();
        }

        public async Task<PatientRegistration> Registration(PatientRegistration patient)
        {
            db.PatientRegistrations.Add(patient);
            await db.SaveChangesAsync();
            return patient;
        }

        public async Task<PatientRegistration> Updatedetails(PatientRegistration patient)
        {
            db.Update(patient);
            await db.SaveChangesAsync();
            return patient;
        }
    }
}
