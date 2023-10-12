using AutoMapper;
using EmployeeManager.Core.Dto;
using EmployeeManager.Core.IRepository;
using EmployeeManager.Core.Service.Interface;
using EmployeeManager.Data.Data;
using Ganss.Xss;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Core.Service.Implementaion
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentService> _logger;
        private readonly IMapper _mapper;
        private readonly HtmlSanitizer _sanitizer;

        public DepartmentService(IUnitOfWork unitOfWork,
            ILogger<DepartmentService> logger,
            IMapper mapper,
            HtmlSanitizer sanitizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _sanitizer = sanitizer;
        }

        public async Task<ResponseDto> CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var responseDto = new ResponseDto();

            var sanitizedRequest = new CreateDepartmentDto
            {
                DepartmentLocation = _sanitizer.Sanitize(createDepartmentDto.DepartmentLocation),
                DepartmentName = _sanitizer.Sanitize(createDepartmentDto.DepartmentName)
            };

            var department = await _unitOfWork.Departments.GetAsync(q => q.DepartmentName == sanitizedRequest.DepartmentName);
            if (department == null)
            {
                var departmentMap = _mapper.Map<Department>(sanitizedRequest);
                await _unitOfWork.Departments.InsertAsync(departmentMap);
                var result = await _unitOfWork.SaveAsync();

                if (result == 1)
                {
                    responseDto.StatusCode = 200;
                    responseDto.Message = "Department Successfully Created.";
                }
                else
                {
                    responseDto.StatusCode = 404;
                    responseDto.Message = "Department Not Created.";
                }
            }
            else
            {
                responseDto.StatusCode = 405;
                responseDto.Message = $"Department Already Exit 'HotelName: {sanitizedRequest.DepartmentName}'.";
            }

            return responseDto;
        }

        public async Task<ResponseDto> DeleteDepartment(long Id)
        {
            var responseDto = new ResponseDto();

            if (Id < 1)
            {
                _logger.LogInformation($"Invalid DELETE attempt in {nameof(DeleteDepartment)}");
                responseDto.StatusCode = 500;
                responseDto.Message = "Invalid Id.";
                return responseDto;
            }

            var department = await _unitOfWork.Departments.GetAsync(q => q.Id == Id);
            if (department == null)
            {
                _logger.LogInformation($"Invalid DELETE attempt in {nameof(DeleteDepartment)}");
                responseDto.StatusCode = 500;
                responseDto.Message = "Department with Id does not exit.";
                return responseDto;
            }

            await _unitOfWork.Departments.DeleteAsync(Id);
            var result = await _unitOfWork.SaveAsync();
            if (result == 1)
            {
                responseDto.StatusCode = 200;
                responseDto.Message = "Department Successfully Deleted.";
            }
            else
            {
                responseDto.StatusCode = 404;
                responseDto.Message = "Department Not Deleted.";
            }

            return responseDto;
        }

        public async Task<DepartmentResultDto> GetDepartment(long Id)
        {
            var departmentDto = new DepartmentResultDto();

            if (Id < 1)
            {
                departmentDto.StatusCode = 500;
                departmentDto.Message = "Invalid Id.";
                return departmentDto;
            }

            var hotel = await _unitOfWork.Departments.GetAsync(x => x.Id == Id);
            var result = _mapper.Map<DepartmentDto>(hotel);
            if (result != null)
            {
                departmentDto.StatusCode = 200;
                departmentDto.Message = "Department Successfully Loaded.";
                departmentDto.Departments = new List<DepartmentDto> { result };
            }
            else
            {
                departmentDto.StatusCode = 404;
                departmentDto.Message = "Department Not Loaded.";
            }

            return departmentDto;
        }

        public async Task<DepartmentResultDto> GetDepartments()
        {
            var departmentDto = new DepartmentResultDto();

            var departments = await _unitOfWork.Departments.GetAllAsync();
            var results = _mapper.Map<List<DepartmentDto>>(departments);
            if (results.Count > 0)
            {
                departmentDto.StatusCode = 200;
                departmentDto.Message = "Departments Successfully Loaded.";
                departmentDto.Departments = results;
            }
            else
            {
                departmentDto.StatusCode = 404;
                departmentDto.Message = "Departments Not Loaded.";
            }

            return departmentDto;
        }

        public async Task<DepartmentResultDto> GetDepartments(RequestParamDto requestParamDto)
        {
            var departmentDto = new DepartmentResultDto();

            var departments = await _unitOfWork.Departments.GetAllPagenationAsync(requestParamDto);
            var results = _mapper.Map<List<DepartmentDto>>(departments);

            if (results.Count > 0)
            {
                departmentDto.StatusCode = 200;
                departmentDto.Message = "Departments Successfully Loaded.";
                departmentDto.Departments = results;
            }
            else
            {
                departmentDto.StatusCode = 404;
                departmentDto.Message = "Departments Not Loaded.";
            }

            return departmentDto;
        }

        public async Task<ResponseDto> UpdateDepartment(long Id, UpdateDepartmentDto departmentDto)
        {
            var responseDto = new ResponseDto();

            if (Id < 1)
            {
                responseDto.StatusCode = 500;
                responseDto.Message = "Invalid Id.";
                return responseDto;
            }

            if (departmentDto == null)
            {
                responseDto.StatusCode = 500;
                responseDto.Message = "Invalid Fields.";
                return responseDto;
            }

            var sanitizedRequest = new UpdateDepartmentDto
            {
                DepartmentLocation = _sanitizer.Sanitize(departmentDto.DepartmentLocation),
                DepartmentName = _sanitizer.Sanitize(departmentDto.DepartmentName)
            };

            var department = await _unitOfWork.Departments.GetAsync(q => q.Id == Id);
            if (department == null)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateDepartment)}");
                responseDto.StatusCode = 500;
                responseDto.Message = "Submited data is invalid";
                return responseDto;
            }

            _mapper.Map(sanitizedRequest, department);
            _unitOfWork.Departments.Update(department);

            var result = await _unitOfWork.SaveAsync();
            if (result == 1)
            {
                responseDto.StatusCode = 200;
                responseDto.Message = "Department Successfully Updated.";
            }
            else
            {
                responseDto.StatusCode = 404;
                responseDto.Message = "Department Not Updated.";
            }

            return responseDto;
        }
    }
}
