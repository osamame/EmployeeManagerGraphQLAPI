namespace EmployeeManager.Core.Dto
{
    public class CreateSalaryDto
    {
        public int Grade { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
    }

    public class SalaryDto : CreateSalaryDto
    {
        public long Id { get; set; }
    }

    public class UpdateSalaryDto : CreateSalaryDto
    {

    }

    public class SalaryResultDto : ResponseDto
    {
        public IList<SalaryDto>? Salaries { get; set; }
    }
}
