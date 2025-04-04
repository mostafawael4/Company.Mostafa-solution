using Company.Mostafa.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mostafa.BLL.Interfaces
{
    public interface IEmployee : IGenericRepository<Employee>
    {
       
        Task<IEnumerable<Employee>> GetByNameAsync(string name);
    }
}

