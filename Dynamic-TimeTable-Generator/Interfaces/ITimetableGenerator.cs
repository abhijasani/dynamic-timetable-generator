using TimetableGenerator.Models;

namespace TimetableGenerator.Interfaces
{
    /// <summary>
    /// Defines methods for generating a timetable.
    /// </summary>
    public interface ITimetableGenerator
    {
        /// <summary>
        /// Generates a timetable based on the provided configuration and subjects.
        /// </summary>
        string[,] GenerateTimetable(TimetableConfiguration configuration, List<Subject> subjects);
    }
}