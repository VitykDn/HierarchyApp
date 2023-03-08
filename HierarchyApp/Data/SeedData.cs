using Bogus;
using HierarchyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HierarchyApp.Data
{
    namespace HierarchyApp.Data
    {
        public class SeedData
        {
            private readonly ApplicationDbContext _context;
            private Faker faker = new Faker();

            public SeedData(ApplicationDbContext context)
            {
                _context = context;
            }
                public static void CreateHierarchy(ApplicationDbContext context)
            {
                Faker faker = new Faker();
                var employeeList = context.Employees.ToList();
                foreach (var employee in employeeList)
                {
                    employee.BossId = GenerateBossId(employeeList, faker, employee.CompanyPositionId);
                }
                context.Employees.UpdateRange(employeeList);
                context.SaveChanges();
            }


            private static   int? GenerateBossId(List<Employee> employeeList, Faker? faker, int? PositionId)
            {
                if (PositionId == 0)
                {
                    return null;
                }
                else
                {
                    var employeesWithHigherPosition = employeeList.Where(e => e.CompanyPositionId == PositionId - 1).Select(e => e.EmployeeId).ToList();
                    if (employeesWithHigherPosition.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        var bossEmployee = faker.PickRandom(employeesWithHigherPosition);
                        return bossEmployee;

                    }
                }
            }
            public static void CreateEmployees(ApplicationDbContext context,int empoyeesNumber)
            {
                int employeeIds = 0;
                var position = context.CompanyPositions.ToList();
                var testObject = new Faker<Employee>()
                    .CustomInstantiator(f => new Employee(employeeIds))
                    .RuleFor(e => e.FullName, (f, e) => f.Name.FullName())
                    .RuleFor(e => e.Image, f => "user-icon.png")
                    .RuleFor(e => e.Salary, f => f.Random.Decimal(1000, 50000))
                    .RuleFor(e => e.StartDate, (f, e) => f.Date.Recent(500))
                    .RuleFor(e => e.CompanyPosition, (f, e) => f.PickRandom(position))
                    ;
                var testList = testObject.Generate(empoyeesNumber);
                var employeeList = testList;
                Faker fak = new Faker();
                foreach (var item in testList)
                {
                    item.Position = item.CompanyPosition.PositionName;
                    item.CompanyPositionId = item.CompanyPosition.CompanyPositionId;
                }
                var seedDatalist = testList;
                context.Employees.AddRange(testList);
                context.SaveChanges();
            }
            public static void CreatePositions(ApplicationDbContext context)
                {
                    context.Database.Migrate();

                    if (!context.CompanyPositions.Any())
                    {
                        context.CompanyPositions.AddRange(

                            new CompanyPosition() { PositionName = "CIO" },
                            new CompanyPosition() { PositionName = "Regional Manager" },
                            new CompanyPosition() { PositionName = "Manager" },
                            new CompanyPosition() { PositionName = "Developer" },
                            new CompanyPosition() { PositionName = "Tester" },
                            new CompanyPosition() { PositionName = "That Guy" },
                            new CompanyPosition() { PositionName = "Jeff" }
                        );

                        context.SaveChanges();
                    }
                }
        }
    }
}