using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Models
{
    public class EmployeeByCompanyContext : DbContext
    {
        public EmployeeByCompanyContext(DbContextOptions<EmployeeByCompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
