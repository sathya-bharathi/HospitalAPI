using System;
using System.Collections.Generic;

namespace HospitalAPI.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            DoctorRegistrations = new HashSet<DoctorRegistration>();
        }

        public int SpecializationId { get; set; }
        public string? SpecializationName { get; set; }

        public virtual ICollection<DoctorRegistration> DoctorRegistrations { get; set; }
    }
}
