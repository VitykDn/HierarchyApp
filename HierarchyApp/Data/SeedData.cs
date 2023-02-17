using Bogus;
using HierarchyApp.Models;
using Bogus.DataSets;
using Bogus.Extensions;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace HierarchyApp.Data
{
    namespace HierarchyApp.Data
    {
        public class SeedData
        {
            //public static void CreateSeedData(ApplicationDbContext context)
            //{
            //    int employeeIds = 0;
            //    var position = context.CompanyPositions.ToList();
            //    Randomizer.Seed = new Random(3897234);
            //    var testObject = new Faker<Employee>()
            //        .CustomInstantiator(f => new Employee(employeeIds))
            //        .RuleFor(e => e.EmployeeId, f =>employeeIds++)
            //        .RuleFor(e => e.FullName, (f, e) => f.Name.FullName())
            //        .RuleFor(e => e.Image, f => f.Internet.Avatar())
            //        .RuleFor(e => e.Salary, f => f.Random.Decimal(1000,50000))
            //        .RuleFor(e => e.StartDate, (f,e) => f.Date.Recent(500))
            //        .RuleFor(e => e.CompanyPosition, f => f.PickRandom(position))
            //        .RuleFor(e => e.CompanyPositionId, e => e.)
            //        .
            //    ; 

            //}
            public static void SeedDatabase(ApplicationDbContext context)
            {
                context.Database.Migrate();

                if (!context.CompanyPositions.Any())
                {

                    context.CompanyPositions.AddRange(

                        new CompanyPosition() { PositionName = "CIO" },
                        new CompanyPosition() { PositionName = "Regional Manager" },
                        new CompanyPosition() { PositionName = "Manager" },
                        new CompanyPosition() { PositionName = "Developer," },
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
