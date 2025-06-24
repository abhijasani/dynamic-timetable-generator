using Microsoft.AspNetCore.Mvc;
using TimetableGenerator.Interfaces;
using TimetableGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace TimetableGenerator.Controllers
{
    
    
    /// <summary>
    /// Controller for managing timetable operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;
        private readonly ILogger<TimetableController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimetableController"/> class.
        /// </summary>
        /// <param name="timetableService">The timetable service to be used.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        public TimetableController(
            ITimetableService timetableService,
            ILogger<TimetableController> logger)
        {
            _timetableService = timetableService ?? throw new ArgumentNullException(nameof(timetableService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Validates the timetable configuration
        /// </summary>
        /// <param name="configuration">The timetable configuration to validate</param>
        /// <returns>Validation result</returns>
        [HttpPost("validate-configuration")]
        public async Task<IActionResult> ValidateConfiguration([FromBody] TimetableConfiguration configuration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isValid = await _timetableService.ValidateConfigurationAsync(configuration);
                return Ok(new { IsValid = isValid, TotalHours = configuration.TotalHoursPerWeek });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating configuration");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Generates a timetable based on the provided configuration and subjects
        /// </summary>
        /// <param name="request">The timetable generation request</param>
        /// <returns>Generated timetable</returns>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateTimetable([FromBody] TimetableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _timetableService.GenerateTimetableAsync(request);
                
                if (!result.IsSuccess)
                {
                    return BadRequest(new { Message = result.Message });
                }

                return Ok(new
                {
                    Success = result.IsSuccess,
                    Message = result.Message,
                    Timetable = result.GetTimetableAsJaggedArray(),
                    Rows = result.Rows,
                    Columns = result.Columns
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating timetable");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets the total hours calculation for given configuration
        /// </summary>
        /// <param name="workingDays">Number of working days</param>
        /// <param name="subjectsPerDay">Number of subjects per day</param>
        /// <returns>Total hours calculation</returns>
        [HttpGet("calculate-total-hours")]
        public IActionResult CalculateTotalHours([Range(1, 7)] int workingDays, [Range(1, 8)] int subjectsPerDay)
        {
            var totalHours = workingDays * subjectsPerDay;
            return Ok(new { TotalHours = totalHours });
        }
    }
}