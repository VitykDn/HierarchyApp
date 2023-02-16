using HierarchyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HierarchyApp.Data
{
    namespace HierarchyApp.Data
    {
        public class SeedData
        {
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
