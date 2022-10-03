using System;
using System.Collections.Generic;

namespace HospitalAPI.Models
{
    public partial class AppointmentBooking
    {
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }

        public virtual DoctorRegistration? Doctor { get; set; }
        public virtual PatientRegistration? Patient { get; set; }
    }
}
