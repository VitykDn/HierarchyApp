using HierarchyApp.Data;
using HierarchyApp.Data.Implementation;
using HierarchyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HierarchyApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ICompanyPositionRepository _companyPositionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeesController(ICompanyPositionRepository companyPositionRepository,
            IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnvironment = webHostEnviroment;
            _companyPositionRepository = companyPositionRepository;
            _employeeRepository = employeeRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("Employee not exist");
            }
            var employee = await _employeeRepository.GetById(id);

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.positionData = await GetPositions();
            ViewBag.employeeData = await GetEmployees();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.Creat(employee);
                return RedirectToAction(nameof(Index));
            }
            //ViewBag.positionData = GetPositions();
            //ViewBag.employeeData = GetEmployees();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var listEmployee = await GetEmployees(id);
            ViewBag.positionData = await GetPositions();
            ViewBag.employeeData = listEmployee;
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetById(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            ViewBag.positionData = GetPositions();
            ViewBag.employeeData = GetEmployees(id);
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                    await _employeeRepository.Update(id, employee);
                    return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ////if (_context.Employees == null)
            ////{
            ////    return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            ////}
            ////var employee = await _context.Employees.FindAsync(id);
            //if (employee != null)
            //{
            //    if (!string.Equals(employee.Image, "noimage.png"))
            //    {
            //        string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
            //        string oldImagePath = Path.Combine(uploadsDir, employee.Image);
            //        if (System.IO.File.Exists(oldImagePath))
            //        {
            //            System.IO.File.Delete(oldImagePath);
            //        }
            //    }
            //    _context.Employees.Remove(employee);
            //}

            //await _context.SaveChangesAsync();
            _employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool EmployeeExists(int id)
        //{
        //    return _employeeRepository.GetById(id);
        //    //return _context.Employees.Any(e => e.EmployeeId == id);
        //}

        private  Task<IEnumerable<Employee>> GetEmployees(int? id = 0)
        {
            if (id != 0)
                return _employeeRepository.GetAllWithExceptionAsync(id);
            else
                return _employeeRepository.GetAllAsync();
        }

        private Task<IEnumerable<CompanyPosition>> GetPositions()
        {
             return _companyPositionRepository.GetAllAsync();

        }
    }
}