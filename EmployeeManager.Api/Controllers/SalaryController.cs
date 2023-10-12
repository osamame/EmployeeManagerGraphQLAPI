using AutoMapper;
using EmployeeManager.Core.Dto;
using EmployeeManager.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManager.Api.Controllers
{
    //[ApiVersion("2.0", Deprecated = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        private readonly IMapper _mapper;
        private readonly ISalaryService _iSalaryService;

        public SalaryController(ILogger<SalaryController> logger,
            IMapper mapper,
            ISalaryService iSalaryService)
        {
            _logger = logger;
            _mapper = mapper;
            _iSalaryService = iSalaryService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalaries() => Ok(await _iSalaryService.GetSalaries());

        [Authorize]
        [HttpGet("{Id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalary(long Id) => Ok(await _iSalaryService.GetSalary(Id));

        [Authorize]
        [HttpGet]
        [Route("get-salaries-pagenation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalaries([FromQuery] RequestParamDto requestParamDto) => Ok(await _iSalaryService.GetSalaries(requestParamDto));

        [Authorize]
        [HttpDelete]
        [Route("delete-salary/{Id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSalary(long Id) => Ok(await _iSalaryService.DeleteSalary(Id));

        [Authorize]
        [HttpPost]
        [Route("create-salary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSalary([FromBody] CreateSalaryDto salaryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid POST attempt in {nameof(CreateSalary)}");
                return BadRequest(ModelState);
            }

            return Ok(await _iSalaryService.CreateSalary(salaryDto));
        }

        [Authorize]
        [HttpPut]
        [Route("update-salary/{Id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSalary(long Id, [FromBody] UpdateSalaryDto salaryDto)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                _logger.LogInformation($"Invalid POST attempt in {nameof(UpdateSalary)}");
                return BadRequest(ModelState);
            }

            return Ok(await _iSalaryService.UpdateSalary(Id, salaryDto));
        }
    }
}
