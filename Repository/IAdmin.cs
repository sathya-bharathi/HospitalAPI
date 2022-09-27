using HospitalAPI.Models;
using HospitalAPI.Token;

namespace HospitalAPI.Repository
{
    public interface IAdmin
    {
        //public Task<Admin> Login(Admin admin);
        public Task<AdminToken> Login(Admin admin);
        public Task<DoctorRegistration> Register(DoctorRegistration doctor);

    }
}
