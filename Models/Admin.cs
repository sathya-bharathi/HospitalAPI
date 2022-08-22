using System;
using System.Collections.Generic;

namespace HospitalAPI.Models
{
    public partial class Admin
    {
        public string AdminId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
