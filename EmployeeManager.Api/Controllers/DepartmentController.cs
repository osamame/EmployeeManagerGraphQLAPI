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
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _iDepartmentService;

        public DepartmentController(ILogger<DepartmentController> logger,
            IMapper mapper,
            IDepartmentService iDepartmentService)
        {
            _logger = logger;
            _mapper = mapper;
            _iDepartmentService = iDepartmentService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartments() => Ok(await _iDepartmentService.GetDepartments());

        [Authorize]
        [HttpGet("{Id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartment(long Id) => Ok(await _iDepartmentService.GetDepartment(Id));

        [Authorize]
        [HttpDelete]
        [Route("delete-department/{Id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment(long Id) => Ok(await _iDepartmentService.DeleteDepartment(Id));

        [Authorize]
        [HttpGet]
        [Route("get-department-pagenation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartments([FromQuery] RequestParamDto requestParamDto) => Ok(await _iDepartmentService.GetDepartments(requestParamDto));

        [Authorize]
        [HttpPost]
        [Route("create-department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto createCountryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid POST attempt in {nameof(CreateDepartment)}");
                return BadRequest(ModelState);
            }

            return Ok(await _iDepartmentService.CreateDepartment(createCountryDto));
        }

        [Authorize]
        [HttpPut]
        [Route("update-department/{Id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDepartment(long Id, [FromBody] UpdateDepartmentDto countryDto)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                _logger.LogInformation($"Invalid POST attempt in {nameof(UpdateDepartment)}");
                return BadRequest(ModelState);
            }

            return Ok(await _iDepartmentService.UpdateDepartment(Id, countryDto));
        }
    }
}
