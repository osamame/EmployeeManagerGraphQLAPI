using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Data.Data
{
    public class Salary
    {
        [Key]
        public long Id { get; set; }
        public int Grade { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
