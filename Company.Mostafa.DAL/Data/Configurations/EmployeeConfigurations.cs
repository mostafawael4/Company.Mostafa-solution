using Company.Mostafa.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mostafa.DAL.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(b => b.Salary).HasColumnType("decimal(10,2)");
            //builder.HasOne(E => E.WorkFor).WithMany(D => D.Employees).HasForeignKey(E => E.DepId);
        }
    }
}

