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
            var existingEmployee = await GetById(id);
            if (existingEmployee == null)
                throw new ArgumentException("Employee does not exist", nameof(id));

            if (!string.Equals(existingEmployee.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string oldImagePath = Path.Combine(uploadsDir, existingEmployee.Image);
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
            //Deleting this BossId in child nodes
            var newBossesId = await _context.Employees
                .Where(e => e.CompanyPositionId == existingEmployee.CompanyPositionId).Select(e => e.EmployeeId)
                .ToListAsync();
            Random random = new Random();
            var childNodes = await _context.Employees.Where(e => e.BossId == id).ToListAsync();
            childNodes.ForEach(e => e.BossId = newBossesId[random.Next(newBossesId.Count)]);

            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task<IEnumerable<Employee>> GetAllWithExceptionAsync(int? id) => await _context.Employees.Where(e => e.EmployeeId != id).ToListAsync();

        public async Task<Employee?> GetById(int? id)
            => id is null ? throw new ArgumentException("Position not exist") : await _context.Employees
            .SingleOrDefaultAsync(p => p.EmployeeId == id);

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
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                throw new ArgumentException("User not exist");
            }

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();

            return existingEmployee;
        }


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