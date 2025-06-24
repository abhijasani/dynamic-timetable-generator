using TimetableGenerator.Models;

namespace TimetableGenerator.Interfaces
{
    /// <summary>
    /// Defines methods for timetable generation and validation.
    /// </summary>
    public interface ITimetableService
    {
        /// <summary>
        /// Generates a timetable based on the provided request.
        /// </summary>
        Task<TimetableResponse> GenerateTimetableAsync(TimetableRequest request);
        /// <summary>
        /// Validates the provided timetable configuration.
        /// </summary>
        Task<bool> ValidateConfigurationAsync(TimetableConfiguration configuration);
    }
}