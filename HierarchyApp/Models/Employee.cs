using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using HierarchyApp.Data.Validation;

namespace HierarchyApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public int? BossId { get; set; }
        public int? CompanyPositionId { get; set; }
        public CompanyPosition? CompanyPosition { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }
        public string? Position { get; set; }
        public string Image { get; set; } = "noimage.png";
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }


        public Employee()
        {

        }
        public Employee(int employeeId)
        {
            this.EmployeeId = employeeId;
        }
    }

}
