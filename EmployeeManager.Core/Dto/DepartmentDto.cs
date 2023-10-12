using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Core.Dto
{
    public class CreateDepartmentDto
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Department Name Is Too Long")]
        public string DepartmentName { get; set; }

        [Required]
        [StringLength(maximumLength: 300, ErrorMessage = "Department Location Is Too Long")]
        public string DepartmentLocation { get; set; }
    }

    public class UpdateDepartmentDto : CreateDepartmentDto
    {

    }

    public class DepartmentDto : CreateDepartmentDto
    {
        public long Id { get; set; }
    }

    public class DepartmentResultDto : ResponseDto
    {
        public List<DepartmentDto>? Departments { get; set; }
    }
}
