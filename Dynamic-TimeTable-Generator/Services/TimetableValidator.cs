using TimetableGenerator.Interfaces;
using TimetableGenerator.Models;

namespace TimetableGenerator.Services
{
    /// <summary>
    /// Validates the timetable configuration and subjects.
    /// </summary>
    public class TimetableValidator : ITimetableValidator
    {
        /// <summary>
        /// Validates the timetable configuration.
        /// </summary>
        public bool ValidateConfiguration(TimetableConfiguration configuration)
        {
            return configuration.WorkingDays >= 1 && configuration.WorkingDays <= 7 &&
                   configuration.SubjectsPerDay >= 1 && configuration.SubjectsPerDay < 9 &&
                   configuration.TotalSubjects >= 1;
        }

        /// <summary>
        /// Validates the subjects based on the total hours required.
        /// </summary>
        public bool ValidateSubjects(List<Subject> subjects, int totalHoursRequired)
        {
            if (subjects == null || !subjects.Any())
                return false;

            var totalHours = subjects.Sum(s => s.Hours);
            return totalHours == totalHoursRequired && 
                   subjects.All(s => !string.IsNullOrWhiteSpace(s.Name) && s.Hours > 0);
        }
    }
}