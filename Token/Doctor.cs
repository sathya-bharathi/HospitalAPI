using HospitalAPI.Models;

namespace HospitalAPI.Token
{
    public class DoctorToken
    {
        public DoctorRegistration? doctor { get; set; }
        public string? doctorToken { get; set; }
    }
}