using HospitalAPI.Models;


namespace HospitalAPI.Repository
{
    public interface IAppointmentBooking
    {
        public Task<List<AppointmentBooking>> GetDetails();
        public Task<List<AppointmentBooking>> GetAppointmentDetails(string DoctorId);
        public Task<List<AppointmentBooking>> AppointmentDetails(string PatientId);


    }
}
