using AutoMapper;
using EmployeeManager.Core.Dto;
using EmployeeManager.Data.Data;

namespace EmployeeManager.Core.Configuration
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Salary, SalaryDto>().ReverseMap();
            CreateMap<Salary, CreateSalaryDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        }
    }
}
