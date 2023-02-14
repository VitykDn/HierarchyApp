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
        public string FullName { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string Image { get; set; } = "noimage.png";
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
