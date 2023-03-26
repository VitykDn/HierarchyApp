using HierarchyApp.Models;

namespace HierarchyApp.Data.Implementation
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllAsync();
        public Task<IEnumerable<Employee>> GetAllWithExceptionAsync(int? id);
    }
}
