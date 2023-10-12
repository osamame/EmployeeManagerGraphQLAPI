using EmployeeManager.Core.Dto;

namespace EmployeeManager.Core.Service.Interface
{
    public interface ISalaryService
    {
        Task<SalaryResultDto> GetSalaries();
        Task<SalaryResultDto> GetSalary(long Id);
        Task<ResponseDto> CreateSalary(CreateSalaryDto createSalaryDto);
        Task<ResponseDto> DeleteSalary(long Id);
        Task<ResponseDto> UpdateSalary(long Id, UpdateSalaryDto updateSalaryDto);
        Task<SalaryResultDto> GetSalaries(RequestParamDto requestParamDto);
    }
}
