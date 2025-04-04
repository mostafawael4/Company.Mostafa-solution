using Company.Mostafa.BLL.Interfaces;
using Company.Mostafa.DAL.Data.Contexts;
using Company.Mostafa.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mostafa.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployee
    {
        
        public EmployeeRepository(AppDBContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}

