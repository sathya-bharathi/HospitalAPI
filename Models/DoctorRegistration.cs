using System;
using System.Collections.Generic;

namespace HospitalAPI.Models
{
    public partial class DoctorRegistration
    {
        public DoctorRegistration()
        {
            AppointmentBookings = new HashSet<AppointmentBooking>();
        }

        public string DoctorId { get; set; } = null!;
        public string? DoctorName { get; set; }
        public string? Qualification { get; set; }
        public int? SpecializationId { get; set; }
        public string? Position { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public virtual Specialization? Specialization { get; set; }
        public virtual ICollection<AppointmentBooking> AppointmentBookings { get; set; }
    }
}
