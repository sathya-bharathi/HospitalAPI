using HospitalAPI.Models;
using HospitalAPI.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalAPI.Repository
{
    public class DoctorRepo : IDoctor
    {
        private readonly HMSDbContext db;
        private readonly IConfiguration _configuration;

        public DoctorRepo(HMSDbContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;

        }

        public void DeleteDoctor(string id)
        {
            DoctorRegistration doctor = db.DoctorRegistrations.Find(id);
            db.Remove(doctor);
            db.SaveChanges();
        }

        public async Task<List<DoctorRegistration>> GetDetails()
        {
            return await db.DoctorRegistrations.Include(x => x.Specialization).ToListAsync();
        }

        public async Task<DoctorRegistration> GetDetails(string id)
        {
            return await db.DoctorRegistrations.FindAsync(id);
        }

        //public async Task<DoctorRegistration> Login(DoctorRegistration doctor)
        //{
        //    var Doctor = await (from i in db.DoctorRegistrations where i.DoctorId == doctor.DoctorId && i.Password == doctor.Password select i).SingleOrDefaultAsync();
        //    try
        //    {
        //        if (Doctor != null)
        //        {
        //            return Doctor;
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

        public async Task<DoctorToken> Login(DoctorRegistration doctor)

        {
            DoctorToken ad = new DoctorToken();
            var result = await (from i in db.DoctorRegistrations where i.Password == doctor.Password && i.DoctorId == doctor.DoctorId select i).SingleOrDefaultAsync();
            ad.doctor = result;
            if (result != null)
            {
                var authClaims = new List<Claim>
        { new Claim(ClaimTypes.SerialNumber, doctor.DoctorId), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };

                var token = GetToken(authClaims);
                string s = new JwtSecurityTokenHandler().WriteToken(token);
                ad.doctorToken = s;
                ad.doctor = result;
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

        public async Task<DoctorRegistration> UpdateDetails(DoctorRegistration doctor)
        {
            db.Update(doctor);
            await db.SaveChangesAsync();
            return doctor;

        }
        public bool DoctorRegistrationExists(string id)
        {
            return (db.DoctorRegistrations.Include(x => x.Specialization)?.Any(e => e.DoctorId == id)).GetValueOrDefault();
        }

        
    }
}
