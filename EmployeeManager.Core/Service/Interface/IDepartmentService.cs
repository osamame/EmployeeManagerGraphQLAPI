using EmployeeManager.Core.Dto;

namespace EmployeeManager.Core.Service.Interface
{
    public interface IDepartmentService
    {
        Task<DepartmentResultDto> GetDepartments();
        Task<DepartmentResultDto> GetDepartment(long Id);
        Task<ResponseDto> CreateDepartment(CreateDepartmentDto createDepartmentDto);
        Task<ResponseDto> DeleteDepartment(long Id);
        Task<ResponseDto> UpdateDepartment(long Id, UpdateDepartmentDto departmentDto);
        Task<DepartmentResultDto> GetDepartments(RequestParamDto requestParamDto);
    }
}
