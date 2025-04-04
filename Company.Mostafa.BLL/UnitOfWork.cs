using Company.Mostafa.BLL.Interfaces;
using Company.Mostafa.BLL.Repositories;
using Company.Mostafa.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mostafa.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartment department;
        private IEmployee employee;
        private readonly AppDBContext _context;
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            department=new DepartmentRepository(context);
            employee=new EmployeeRepository(context);
        }

        public IEmployee employeeRepository  => employee; 
        public IDepartment departmentRepository  => department;
    }
}

