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
    public class DepartmentRepository : GenericRepository<Department>,IDepartment
    {
      
        public DepartmentRepository(AppDBContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Department>> GetByNameAsync(string name)
        {
            var result = await _context.Departments.Where(X=>X.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return result;
        }
      
    }
}

