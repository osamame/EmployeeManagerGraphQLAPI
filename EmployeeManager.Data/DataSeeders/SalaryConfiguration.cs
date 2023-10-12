using EmployeeManager.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManager.Data.DataSeeders
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasData(
               new Salary()
               {
                   DateCreated = DateTime.Now,
                   Grade = 1,
                   Id = 1,
                   IsDeleted = false,
                   MaximumSalary = 2000000000.99M,
                   MinimumSalary = 1000000000.99M
               }, new Salary()
               {
                   DateCreated = DateTime.Now,
                   Grade = 2,
                   Id = 2,
                   IsDeleted = false,
                   MaximumSalary = 3000000000.99M,
                   MinimumSalary = 2000000000.99M
               }, new Salary()
               {
                   DateCreated = DateTime.Now,
                   Grade = 3,
                   Id = 3,
                   IsDeleted = false,
                   MaximumSalary = 4000000000.99M,
                   MinimumSalary = 3000000000.99M
               });
        }
    }
}
