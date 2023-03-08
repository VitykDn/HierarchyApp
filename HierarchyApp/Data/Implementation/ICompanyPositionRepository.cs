using HierarchyApp.Models;

namespace HierarchyApp.Data.Implementation
{
    public interface ICompanyPositionRepository : IRepositoryBase<CompanyPosition>
    {
        public Task<IEnumerable<CompanyPosition>> GetAllAsync();
    }
}
