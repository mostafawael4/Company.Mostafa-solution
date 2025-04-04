using Company.Mostafa.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.Mostafa.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Range(25, 60)]
        public int Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; }
        public Department? WorkFor { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}

