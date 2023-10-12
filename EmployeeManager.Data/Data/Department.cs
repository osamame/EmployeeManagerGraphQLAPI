using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Data.Data
{
    public class Department
    {
        [Key]
        public long Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLocation { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
