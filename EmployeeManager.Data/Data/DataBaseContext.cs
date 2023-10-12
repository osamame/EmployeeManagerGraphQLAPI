using HotelManager.Data.DataSeeders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Data.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Model Seeding
            builder.ApplyConfiguration(new SalaryConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
        }
    }
}
