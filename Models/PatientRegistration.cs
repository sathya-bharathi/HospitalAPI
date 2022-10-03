using System;
using System.Collections.Generic;

namespace HospitalAPI.Models
{
    public partial class PatientRegistration
    {
        public PatientRegistration()
        {
            AppointmentBookings = new HashSet<AppointmentBooking>();
        }

        public string PatientId { get; set; } = null!;
        public string? PatientName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<AppointmentBooking> AppointmentBookings { get; set; }
    }
}
