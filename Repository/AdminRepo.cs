using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalAPI.Repository
{
    public class AdminRepo : IAdmin
    {
        private readonly HMSDbContext db;
        private readonly IConfiguration _configuration;
        public AdminRepo(HMSDbContext db, IConfiguration configuration)
        {
           this.db=db;
            _configuration = configuration;
        }
        //public async  Task<Admin> Login(Admin admin)
        //{
        //    var  Admin = await (from i in db.Admins where admin.AdminId == i.AdminId && admin.Password==i.Password select i).SingleOrDefaultAsync();
        //    try

        //    {
        //        if (Admin != null)
        //        {
        //            return Admin;
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

        public async Task<DoctorRegistration> Register(DoctorRegistration doctor)
        {
            db.DoctorRegistrations.Add(doctor);
            await db.SaveChangesAsync();
            return doctor;
        }
        public async Task<AdminToken> Login(Admin admin)

        {
            AdminToken ad = new AdminToken();
            var result = await (from i in db.Admins where i.Password == admin.Password && i.AdminId == admin.AdminId select i).SingleOrDefaultAsync();
            ad.admin = result;
            if (result != null)
            {
                var authClaims = new List<Claim>
        { new Claim(ClaimTypes.SerialNumber, admin.AdminId), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };

                var token = GetToken(authClaims); 
                string s = new JwtSecurityTokenHandler().WriteToken(token);
                ad.adminToken = s; 
                ad.admin = result;
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

    }

}

