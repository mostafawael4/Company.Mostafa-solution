using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mostafa.DAL.Models
{
    public class Employee : BaseEntity
    { 
        
        
        public string Name { get; set; }
       
        public int Age { get; set; }
        
            
        public string Address { get; set; }
       
        public double Salary { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; }= DateTime.Now;
        public int? WorkForId { get; set; }
        public Department? WorkFor { get; set; }
        public string? ImageName { get; set; }
    }
}

