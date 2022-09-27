using HospitalAPI.Models;

namespace HospitalAPI.Token
{
    public class AdminToken
    {
        public Admin? admin { get; set; }
        public string? adminToken { get; set; }
    }
}