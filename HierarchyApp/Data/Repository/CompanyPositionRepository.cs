using HierarchyApp.Data.Implementation;
using HierarchyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HierarchyApp.Data.Repository
{
    public class CompanyPositionRepository : ICompanyPositionRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyPositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyPosition> Creat(CompanyPosition company)
        {
            if (company == null)
            {
                throw new ArgumentException("Model not exist");
            }
            await _context.CompanyPositions.AddAsync(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<CompanyPosition> Delete(int id)
        {
            if (id == null)
                throw new ArgumentException("Id not exist");
            var companyPosition = await GetById(id);
            if (companyPosition == null)
                throw new ArgumentException("Position not exist");
            _context.CompanyPositions.Remove(companyPosition);
           await _context.SaveChangesAsync();
            return companyPosition;
        }

        public async Task<IEnumerable<CompanyPosition>> GetAllAsync() => await _context.CompanyPositions.ToListAsync();

        public async Task<CompanyPosition?> GetById(int? id)
            => id is null ? throw new ArgumentException("Position not exist") : await _context.CompanyPositions
            .FirstOrDefaultAsync(p => p.CompanyPositionId == id);

        public async Task<CompanyPosition> Update(int id, CompanyPosition company)
        {

            if  (await GetById(id) == null)
            {
                throw new ArgumentException("Position not exist");
            }
              var result =  _context.CompanyPositions.Update(company);
            await _context.SaveChangesAsync();
             return   result.Entity;
        }
    }
}
