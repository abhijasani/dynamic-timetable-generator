using TimetableGenerator.Interfaces;
using TimetableGenerator.Models;

namespace TimetableGenerator.Services
{
    /// <summary>
    /// Service for generating and validating timetables.
    /// </summary>
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableValidator _validator;
        private readonly ITimetableGenerator _generator;
        private readonly ILogger<TimetableService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimetableService"/> class.
        /// </summary>
        public TimetableService(
            ITimetableValidator validator,
            ITimetableGenerator generator,
            ILogger<TimetableService> logger)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Generates a timetable based on the provided request.
        /// </summary>
        public async Task<TimetableResponse> GenerateTimetableAsync(TimetableRequest request)
        {
            try
            {
                _logger.LogInformation("Starting timetable generation");

                if (!_validator.ValidateConfiguration(request.Configuration))
                {
                    return new TimetableResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid configuration parameters"
                    };
                }

                if (!_validator.ValidateSubjects(request.Subjects, request.Configuration.TotalHoursPerWeek))
                {
                    return new TimetableResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid subjects or hours do not match total required hours"
                    };
                }

                var timetable = await Task.Run(() => 
                    _generator.GenerateTimetable(request.Configuration, request.Subjects));

                _logger.LogInformation("Timetable generated successfully");

                return new TimetableResponse
                {
                    Timetable = timetable,
                    Rows = request.Configuration.SubjectsPerDay,
                    Columns = request.Configuration.WorkingDays,
                    IsSuccess = true,
                    Message = "Timetable generated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating timetable");
                return new TimetableResponse
                {
                    IsSuccess = false,
                    Message = "An error occurred while generating the timetable"
                };
            }
        }

        /// <summary>
        /// Validates the timetable configuration asynchronously.
        /// </summary>
        public async Task<bool> ValidateConfigurationAsync(TimetableConfiguration configuration)
        {
            return await Task.FromResult(_validator.ValidateConfiguration(configuration));
        }
    }
}