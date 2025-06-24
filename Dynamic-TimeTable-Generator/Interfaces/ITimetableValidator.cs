using TimetableGenerator.Models;

namespace TimetableGenerator.Interfaces
{
    /// <summary>
    /// Interface for validating timetable configurations and subjects.
    /// </summary>
    public interface ITimetableValidator
    {
        /// <summary>
        /// Validates the timetable configuration.
        /// </summary>
        bool ValidateConfiguration(TimetableConfiguration configuration);

        /// <summary>
        /// Validates the list of subjects against the total hours required.
        /// </summary>
        bool ValidateSubjects(List<Subject> subjects, int totalHoursRequired);
    }
}