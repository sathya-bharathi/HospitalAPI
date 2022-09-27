using HospitalAPI.Models;

namespace HospitalAPI.Token
{
    public class PatientToken
    {
        public PatientRegistration? patient { get; set; }
        public string? patientToken { get; set; }
    }
}