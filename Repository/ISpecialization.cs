using HospitalAPI.Models;

namespace HospitalAPI.Repository
{
    public interface ISpecialization
    {
        public Task<List<Specialization>> GetSpecializations();
        public Task<Specialization> AddSpecialization(Specialization specialization);
        public Task<Specialization> UpdateSpecialization(Specialization specialization);
        public bool SpecializationExists(int id);
        public void DeleteSpecialization(int id);
        public Task<Specialization> GetSpecialization(int id);




    }
}
