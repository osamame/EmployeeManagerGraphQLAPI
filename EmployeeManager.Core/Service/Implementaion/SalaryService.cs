using AutoMapper;
using EmployeeManager.Core.Dto;
using EmployeeManager.Core.IRepository;
using EmployeeManager.Core.Service.Interface;
using EmployeeManager.Data.Data;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Core.Service.Implementaion
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SalaryService> _logger;
        private readonly IMapper _mapper;

        public SalaryService(IUnitOfWork unitOfWork,
            ILogger<SalaryService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SalaryResultDto> GetSalaries()
        {
            var salaryDto = new SalaryResultDto();

            var countries = await _unitOfWork.Salaries.GetAllAsync();
            var results = _mapper.Map<IList<SalaryDto>>(countries);
            if (results.Count > 0)
            {
                salaryDto.StatusCode = 200;
                salaryDto.Message = "Salaries Successfully Loaded.";
                salaryDto.Salaries = results;
            }
            else
            {
                salaryDto.StatusCode = 404;
                salaryDto.Message = "Salaries Not Loaded.";
            }

            return salaryDto;
        }

        public async Task<SalaryResultDto> GetSalary(long Id)
        {
            var salaryDto = new SalaryResultDto();

            if (Id < 1)
            {
                salaryDto.StatusCode = 500;
                salaryDto.Message = "Invalid Id.";
                return salaryDto;
            }

            var salary = await _unitOfWork.Salaries.GetAsync(x => x.Id == Id);
            var result = _mapper.Map<SalaryDto>(salary);
            if (result != null)
            {
                salaryDto.StatusCode = 200;
                salaryDto.Message = "Salary Successfully Loaded.";
                salaryDto.Salaries = new List<SalaryDto> { result };
            }
            else
            {
                salaryDto.StatusCode = 404;
                salaryDto.Message = "Salary Not Loaded.";
            }

            return salaryDto;
        }

        public async Task<ResponseDto> CreateSalary(CreateSalaryDto createSalaryDto)
        {
            var responseDto = new ResponseDto();

            var salaryMap = _mapper.Map<Salary>(createSalaryDto);
            await _unitOfWork.Salaries.InsertAsync(salaryMap);
            var result = await _unitOfWork.SaveAsync();

            if (result == 1)
            {
                responseDto.StatusCode = 200;
                responseDto.Message = "Salary Successfully Created.";
            }
            else
            {
                responseDto.StatusCode = 404;
                responseDto.Message = "Salary Not Created.";
            }

            return responseDto;
        }

        public async Task<ResponseDto> DeleteSalary(long Id)
        {
            var responseDto = new ResponseDto();

            if (Id < 1)
            {
                _logger.LogInformation($"Invalid DELETE attempt in {nameof(DeleteSalary)}");
                responseDto.StatusCode = 200;
                responseDto.Message = "Invalid Id.";
                return responseDto;
            }

            var salary = await _unitOfWork.Salaries.GetAsync(q => q.Id == Id);
            if (salary == null)
            {
                _logger.LogInformation($"Invalid DELETE attempt in {nameof(DeleteSalary)}");
                responseDto.StatusCode = 200;
                responseDto.Message = "Salary with Id does not exit.";
                return responseDto;
            }

            await _unitOfWork.Salaries.DeleteAsync(Id);
            var result = await _unitOfWork.SaveAsync();
            if (result == 1)
            {
                responseDto.StatusCode = 200;
                responseDto.Message = "Salary Successfully Deleted.";
            }
            else
            {
                responseDto.StatusCode = 404;
                responseDto.Message = "Salary Not Deleted.";
            }

            return responseDto;
        }

        public async Task<ResponseDto> UpdateSalary(long Id, UpdateSalaryDto countryDto)
        {
            var responseDto = new ResponseDto();

            if (Id < 1)
            {
                responseDto.StatusCode = 500;
                responseDto.Message = "Invalid Id.";
                return responseDto;
            }

            if (responseDto == null)
            {
                responseDto.StatusCode = 500;
                responseDto.Message = "Invalid Fields.";
                return responseDto;
            }

            var salary = await _unitOfWork.Salaries.GetAsync(q => q.Id == Id);
            if (salary == null)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateSalary)}");
                responseDto.StatusCode = 200;
                responseDto.Message = "Submited data is invalid";
                return responseDto;
            }

            _mapper.Map(countryDto, salary);
            _unitOfWork.Salaries.Update(salary);

            var result = await _unitOfWork.SaveAsync();
            if (result == 1)
            {
                responseDto.StatusCode = 200;
                responseDto.Message = "Salary Successfully Updated.";
            }
            else
            {
                responseDto.StatusCode = 404;
                responseDto.Message = "Salary Not Updated.";
            }

            return responseDto;
        }

        public async Task<SalaryResultDto> GetSalaries(RequestParamDto requestParamDto)
        {
            var salaryDto = new SalaryResultDto();

            var countries = await _unitOfWork.Salaries.GetAllPagenationAsync(requestParamDto);
            var results = _mapper.Map<IList<SalaryDto>>(countries);

            if (results.Count > 0)
            {
                salaryDto.StatusCode = 200;
                salaryDto.Message = "Salaries Successfully Loaded.";
                salaryDto.Salaries = results;
            }
            else
            {
                salaryDto.StatusCode = 404;
                salaryDto.Message = "Salaries Not Loaded.";
            }

            return salaryDto;
        }
    }
}
