using Company.Mostafa.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mostafa.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployee employeeRepository { get;  }
        public IDepartment departmentRepository { get; }
    }
}

