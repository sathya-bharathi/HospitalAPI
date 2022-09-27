using HospitalAPI.Models;
using HospitalAPI.Token;

namespace HospitalAPI.Repository
{
    public interface IPatient
    {
        public Task<PatientRegistration> Registration(PatientRegistration patient);
        //public Task<PatientRegistration> Login(PatientRegistration Patient);
        public Task<PatientToken> Login(PatientRegistration patient);
        public Task<AppointmentBooking> BookAppointment(AppointmentBooking appointment);
        public Task<DoctorRegistration> GetDoctordetail(string DoctorId);
        public Task<PatientRegistration> GetPatientdetail(string PatientId);
        public  Task<List<PatientRegistration>> GetDetails();
        public Task<PatientRegistration> Updatedetails(PatientRegistration patient);
        public bool PatientRegistrationExists(string id);





    }
}
