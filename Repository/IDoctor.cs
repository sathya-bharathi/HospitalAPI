using HospitalAPI.Models;
using HospitalAPI.Token;

namespace HospitalAPI.Repository
{
    public interface IDoctor
    {
        //public Task<DoctorRegistration> Login(DoctorRegistration doctor);
        public Task<DoctorToken> Login(DoctorRegistration doctor);

        public Task<List<DoctorRegistration>> GetDetails();
        public Task<DoctorRegistration> UpdateDetails(DoctorRegistration doctor);
        public void DeleteDoctor(string id);
        public bool DoctorRegistrationExists(string id);

        public Task<DoctorRegistration> GetDetails(string id);

    }
}
