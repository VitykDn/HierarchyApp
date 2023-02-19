using HierarchyApp.Data.Implementation;
using HierarchyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HierarchyApp.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<Employee> Creat(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException("Model not exist");
            }
            var position = await _context.CompanyPositions.FirstAsync(c => c.CompanyPositionId == employee.CompanyPositionId);
            employee.Position = position.PositionName;

            if (employee.ImageUpload != null)
            {
                employee.Image = await AddImage(employee.ImageUpload);
            }

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id not specified");
            if (id == null)
                throw new ArgumentException("Id not exist");
            var employee = await GetById(id);
            if (employee == null)
                throw new ArgumentException("Employee does not exist", nameof(id));
            if (!string.Equals(employee.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string oldImagePath = Path.Combine(uploadsDir, employee.Image);
                if (File.Exists(oldImagePath))
                    try
                    {
                        File.Delete(oldImagePath);
                    }
                    catch (IOException ex)
                    {
                        throw new Exception($"Error deleting file: {ex.Message}");
                    }
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task<IEnumerable<Employee>> GetAllWithExceptionAsync(int? id) => await _context.Employees.Where(e => e.EmployeeId != id).ToListAsync();

        public async Task<Employee?> GetById(int? id)
            => id is null ? throw new ArgumentException("Position not exist") : await _context.Employees
            .FirstOrDefaultAsync(p => p.EmployeeId == id);

        public async Task<Employee> Update(int id, Employee employee)
        {
            var position = await _context.CompanyPositions.FirstAsync(c => c.CompanyPositionId == employee.CompanyPositionId);
            employee.Position = position.PositionName;

            if (employee.ImageUpload != null)
            {
                employee.Image = await AddImage(employee.ImageUpload);
            }

            if (await GetById(id) == null)
            {
                throw new ArgumentException("User not exist");
            }

            var result = _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// Adding Image To Emoloyee
        /// Badly Written
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private async Task<string> AddImage(IFormFile imageUpload)
        {
            string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/images");
            string imageName = Guid.NewGuid().ToString() + "_" + imageUpload.FileName;
            string filePath = Path.Combine(uploadsDir, imageName);

            using var fs = new FileStream(filePath, FileMode.Create);
            await imageUpload.CopyToAsync(fs);

            return imageName;
        }
    }
}