using EmployeeManager.Core.Dto;
using EmployeeManager.Core.Service.Interface;

namespace EmployeeManager.Api.GraphQLServices
{
    public class Query
    {
        private IDepartmentService _iDepartmentService;
        private ISalaryService _iSalaryService;

        public Query(IDepartmentService iDepartmentService, ISalaryService iSalaryService)
        {
            _iDepartmentService = iDepartmentService;
            _iSalaryService = iSalaryService;
        }

        public async Task<DepartmentResultDto> GetDepartmentsAsync() => await _iDepartmentService.GetDepartments();

        public async Task<DepartmentResultDto> GetDepartmentByIdAsync(long Id) => await _iDepartmentService.GetDepartment(Id);

        public async Task<DepartmentResultDto> GetDepartmentByPagenationAsync(RequestParamDto requestParamDto) => await _iDepartmentService.GetDepartments(requestParamDto);

        public async Task<SalaryResultDto> GetSalariesAsync() => await _iSalaryService.GetSalaries();

        public async Task<SalaryResultDto> GetSalaryByIdAsync(long Id) => await _iSalaryService.GetSalary(Id);

        public async Task<SalaryResultDto> GetSalariesByPagenationAsync(RequestParamDto requestParamDto) => await _iSalaryService.GetSalaries(requestParamDto);
    }
}
