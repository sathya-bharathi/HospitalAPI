using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repository
{
    public class SpecializationRepo : ISpecialization
    {
        private readonly HMSDbContext db;
        public SpecializationRepo(HMSDbContext db)
        {
            this.db = db;
        }
        public async Task<Specialization> AddSpecialization(Specialization specialization)
        {
            await db.Specializations.AddAsync(specialization);
            await db.SaveChangesAsync();
            return specialization;
        }

        public void DeleteSpecialization(int id)
        {
            Specialization specialization = db.Specializations.Find(id);
            db.Remove(specialization);
            db.SaveChanges();
        }

        public async Task<Specialization> GetSpecialization(int id)
        {
            return await db.Specializations.FindAsync(id);
        }

        public async Task<List<Specialization>> GetSpecializations()
        {
            return await db.Specializations.ToListAsync();
        }

        public bool SpecializationExists(int id)
        {
            return (db.Specializations?.Any(e => e.SpecializationId == id)).GetValueOrDefault();

        }

        public async Task<Specialization> UpdateSpecialization(Specialization specialization)
        {
            db.Update(specialization);
            await db.SaveChangesAsync();
            return specialization;
        }

     
    }
}
