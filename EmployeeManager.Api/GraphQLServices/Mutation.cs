using EmployeeManager.Core.Dto;
using EmployeeManager.Core.Service.Interface;

namespace EmployeeManager.Api.GraphQLServices
{
    public class Mutation
    {
        private IDepartmentService _iDepartmentService;
        private ISalaryService _iSalaryService;

        public Mutation(IDepartmentService iDepartmentService, ISalaryService iSalaryService)
        {
            _iDepartmentService = iDepartmentService;
            _iSalaryService = iSalaryService;
        }

        public async Task<ResponseDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto) => await _iDepartmentService.CreateDepartment(createDepartmentDto);

        public async Task<ResponseDto> UpdateDepartmentAsync(long Id, UpdateDepartmentDto updateDepartmentDto) => await _iDepartmentService.UpdateDepartment(Id, updateDepartmentDto);
      
        public async Task<ResponseDto> DeleteDepartmentAsync(long Id) => await _iDepartmentService.DeleteDepartment(Id);

        public async Task<ResponseDto> CreateSalaryAsync(CreateSalaryDto createSalaryDto) => await _iSalaryService.CreateSalary(createSalaryDto);

        public async Task<ResponseDto> UpdateSalaryAsync(long Id, UpdateSalaryDto updateSalaryDto) => await _iSalaryService.UpdateSalary(Id, updateSalaryDto);

        public async Task<ResponseDto> DeleteSalaryAsync(long Id) => await _iSalaryService.DeleteSalary(Id);
    }
}
