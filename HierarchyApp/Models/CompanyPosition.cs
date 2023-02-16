namespace HierarchyApp.Models
{
    public class CompanyPosition
    {
        public int CompanyPositionId { get; set; }
        public string PositionName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
