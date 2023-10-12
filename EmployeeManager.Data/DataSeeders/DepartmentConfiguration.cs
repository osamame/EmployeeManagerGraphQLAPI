using EmployeeManager.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManager.Data.DataSeeders
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
               new Department()
               {
                   DateCreated = DateTime.Now,
                   DepartmentLocation = "Abuja",
                   DepartmentName = "Human Resource",
                   Id = 1,
                   IsDeleted = false
               },
                new Department()
                {
                    DateCreated = DateTime.Now,
                    DepartmentLocation = "Abuja",
                    DepartmentName = "Admin Department",
                    Id = 2,
                    IsDeleted = false
                },
                 new Department()
                 {
                     DateCreated = DateTime.Now,
                     DepartmentLocation = "Lagos",
                     DepartmentName = "Technical Department",
                     Id = 3,
                     IsDeleted = false
                 }
               );
        }
    }
}
